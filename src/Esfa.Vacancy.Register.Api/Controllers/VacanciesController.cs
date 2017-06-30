﻿using Esfa.Vacancy.Register.Api.Attributes;
using Esfa.Vacancy.Register.Api.Orchestrators;
using MediatR;
using SFA.DAS.NLog.Logger;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Esfa.Vacancy.Register.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/vacancies")]
    public class VacanciesController : ApiController
    {
        private readonly ILog _log;
        private readonly IMediator _mediator;

        public VacanciesController(ILog log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        /// <summary>
        /// Check if a vacancy exists
        /// </summary>
        /// <param name="id"></param>
        [SwaggerOperation("Head")]
        [SwaggerResponse(HttpStatusCode.NoContent)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [ExceptionHandling]
        public async Task Head(int id)
        {
            await Get(id);
        }

        /// <summary>
        /// Get a vacancy by the public vacancy reference identifier
        /// </summary>
        /// <param name="id">The public vacancy reference identifier</param>
        /// <returns>A vacancy for an apprenticeship or a traineeship</returns>
        [SwaggerOperation("Get")]
        [SwaggerResponse(HttpStatusCode.OK, "OK", typeof(Vacancy.Api.Types.Vacancy))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [ExceptionHandling]
        public async Task<IHttpActionResult> Get(int id)
        {
            var vacancyOrchestrator = new VacancyOrchestrator(_mediator);

            var vacancy =  await vacancyOrchestrator.GetVacancyDetailsAsync(id);

            if (vacancy == null)
            {
                return NotFound();
            }

            return Ok(vacancy);
        }

    }
}
