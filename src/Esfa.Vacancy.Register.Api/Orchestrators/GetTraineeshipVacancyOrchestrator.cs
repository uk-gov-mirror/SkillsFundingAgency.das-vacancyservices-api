﻿using System.Threading.Tasks;
using AutoMapper;
using Esfa.Vacancy.Api.Types;
using Esfa.Vacancy.Register.Application.Queries.GetVacancy;
using Esfa.Vacancy.Register.Infrastructure.Settings;
using MediatR;

namespace Esfa.Vacancy.Register.Api.Orchestrators
{
    public class GetTraineeshipVacancyOrchestrator
    {
        private readonly IMediator _mediator;
        private readonly IProvideSettings _provideSettings;

        public GetTraineeshipVacancyOrchestrator(IMediator mediator, IProvideSettings provideSettings)
        {
            _mediator = mediator;
            _provideSettings = provideSettings;
        }

        public async Task<Vacancy.Api.Types.TraineeshipVacancy> GetTraineeshipVacancyDetailsAsync(int id)
        {
            var liveVacancyBaseUrl = _provideSettings.GetSetting(ApplicationSettingKeyConstants.LiveVacancyBaseUrlKey);
            var response = await _mediator.Send(new GetTraineeshipVacancyRequest() { Reference = id });
            var vacancy = MapToTraineeship(response.Vacancy);
            vacancy.VacancyUrl = $"{liveVacancyBaseUrl}/{vacancy.VacancyReference}";
            return vacancy;
        }

        private TraineeshipVacancy MapToTraineeship(Domain.Entities.Vacancy vacancy)
        {
            throw new System.NotImplementedException();
        }
    }
}
