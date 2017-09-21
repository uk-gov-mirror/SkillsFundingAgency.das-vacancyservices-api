﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Esfa.Vacancy.Register.Application.Queries.GetVacancy;
using Esfa.Vacancy.Register.Infrastructure.Settings;
using FluentValidation;
using MediatR;

namespace Esfa.Vacancy.Register.Api.Orchestrators
{
    public class VacancyOrchestrator : IVacancyOrchestrator
    {
        private readonly IMediator _mediator;
        private readonly string _liveVacancyBaseUrl;
        private const string VacPrefix = "VAC";

        public VacancyOrchestrator(IMediator mediator, IProvideSettings provideSettings)
        {
            _mediator = mediator;
            _liveVacancyBaseUrl = provideSettings.GetSetting(ApplicationSettingConstants.LiveVacancyBaseUrl);
        }

        public async Task<Vacancy.Api.Types.Vacancy> GetVacancyDetailsAsync(string id)
        {
            var parsedReference = ParseVacancyReferenceNumber(id);
            var response = await _mediator.Send(new GetVacancyRequest() { Reference = parsedReference });
            var vacancy = Mapper.Map<Vacancy.Api.Types.Vacancy>(response.Vacancy);
            vacancy.VacancyUrl = $"{_liveVacancyBaseUrl}/{vacancy.VacancyReference}";
            return vacancy;
        }

        private static int ParseVacancyReferenceNumber(string rawId)
        {
            var trimmedString = rawId.ToUpper().Replace(VacPrefix.ToUpper(), string.Empty);
            if (!int.TryParse(trimmedString, out var parsedId))
                throw new ValidationException("TODO: get correct wording from product team");
            return parsedId;
        }
    }
}
