﻿using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;
using Esfa.Vacancy.Api.Types;
using Esfa.Vacancy.Register.Application.Exceptions;
using Esfa.Vacancy.Register.Infrastructure.Exceptions;
using FluentValidation;
using SFA.DAS.NLog.Logger;

namespace Esfa.Vacancy.Register.Api.App_Start
{
    public class CustomExceptionHandler : ExceptionHandler
    {
        private const string GenericErrorMessage = "An internal error occurred, please try again.";
        private static readonly ILog Logger = DependencyResolver.Current.GetService<ILog>();

        public override void Handle(ExceptionHandlerContext context)
        {
            if (context.Exception is ValidationException)
            {
                var badrequestResponse = new BadRequestResponse
                {
                    RequestErrors = ((ValidationException)context.Exception).Errors
                        .Select(validationFailure => new BadRequestError
                        {
                            ErrorCode = validationFailure.ErrorCode,
                            ErrorMessage = validationFailure.ErrorMessage
                        })
                };

                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new ObjectContent<BadRequestResponse>(badrequestResponse, new JsonMediaTypeFormatter())
                };

                context.Result = new CustomErrorResult(context.Request, response);

                Logger.Warn(context.Exception, "Validation error");

                return;
            }

            if (context.Exception is UnauthorisedException)
            {
                var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                var message = ((UnauthorisedException)context.Exception).Message;
                response.Content = new StringContent(message);
                context.Result = new CustomErrorResult(context.Request, response);

                Logger.Warn(context.Exception, "Authorisation error");

                return;
            }

            if (context.Exception is ResourceNotFoundException)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound);
                var message = ((ResourceNotFoundException)context.Exception).Message;
                response.Content = new StringContent(message);
                context.Result = new CustomErrorResult(context.Request, response);

                Logger.Warn(context.Exception, "Unable to locate resource error");

                return;
            }

            if (context.Exception is InfrastructureException)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(GenericErrorMessage);
                context.Result = new CustomErrorResult(context.Request, response);

                Logger.Error(context.Exception.InnerException, "Unexpected infrastructure error");

                return;
            }

            Logger.Error(context.Exception, "Unhandled exception");

            base.Handle(context);
        }
    }
}
