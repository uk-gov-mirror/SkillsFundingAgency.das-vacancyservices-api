﻿using System.Threading.Tasks;
using AutoMapper;
using Esfa.Vacancy.Register.Application.Queries.GetVacancy;
using Esfa.Vacancy.Register.Infrastructure.Settings;
using MediatR;

namespace Esfa.Vacancy.Register.Api.Orchestrators
{
    public class VacancyOrchestrator : IVacancyOrchestrator
    {
        private readonly IMediator _mediator;
        private readonly string _liveVacancyBaseUrl;

        public VacancyOrchestrator(IMediator mediator, IProvideSettings provideSettings)
        {
            _mediator = mediator;
            _liveVacancyBaseUrl = provideSettings.GetSetting(ApplicationSettingConstants.LiveVacancyBaseUrl);
        }

        public async Task<Vacancy.Api.Types.Vacancy> GetVacancyDetailsAsync(int id)
        {
            var response = await _mediator.Send(new GetVacancyRequest() { Reference = id });
            var vacancy = response == null ? null : Mapper.Map<Vacancy.Api.Types.Vacancy>(response.Vacancy);

            if (!string.IsNullOrEmpty(_liveVacancyBaseUrl))
            {
                vacancy.VacancyUrl = $"{_liveVacancyBaseUrl}/{vacancy.VacancyReference}";
            }
            return vacancy;
        }
    }
}
