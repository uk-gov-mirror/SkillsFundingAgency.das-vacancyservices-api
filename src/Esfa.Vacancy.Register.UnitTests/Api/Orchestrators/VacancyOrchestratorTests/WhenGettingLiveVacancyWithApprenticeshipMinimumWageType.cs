﻿using System.Threading;
using System.Threading.Tasks;
using Esfa.Vacancy.Api.Types;
using Esfa.Vacancy.Register.Api.Orchestrators;
using Esfa.Vacancy.Register.Application.Queries.GetApprenticeshipVacancy;
using Esfa.Vacancy.Register.Domain.Entities;
using Esfa.Vacancy.Register.Infrastructure.Settings;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Esfa.Vacancy.Register.UnitTests.Api.Orchestrators.VacancyOrchestratorTests
{
    [TestFixture]
    public class WhenGettingLiveVacancyWithApprenticeshipMinimumWageType
    {
        private const int VacancyReference = 1234;
        private const int LiveVacancyStatusId = 2;
        private const string UnknownwWageText = "Unknown";
        private Mock<IMediator> _mockMediator;
        private Mock<IProvideSettings> _provideSettings;
        private GetApprenticeshipVacancyOrchestrator _sut;

        [SetUp]
        public void SetUp()
        {
            _mockMediator = new Mock<IMediator>();
            _provideSettings = new Mock<IProvideSettings>();

            _sut = new GetApprenticeshipVacancyOrchestrator(_mockMediator.Object, _provideSettings.Object);
        }

        [Test]
        public async Task ShouldHaveWageSetForVacancy()
        {
            const decimal minApprenticeshipWageRate = 4.05m;
            const decimal hoursPerWeek = 30;

            _mockMediator.Setup(m => m.Send(It.IsAny<GetApprenticeshipVacancyRequest>(), CancellationToken.None))
                .ReturnsAsync(new GetApprenticeshipVacancyResponse
                {
                    ApprenticeshipVacancy = new Fixture().Build<Domain.Entities.ApprenticeshipVacancy>()
                                            .With(v => v.VacancyReferenceNumber, VacancyReference)
                                            .With(v => v.VacancyStatusId, LiveVacancyStatusId)
                                            .With(v => v.VacancyTypeId, (int)VacancyType.Apprenticeship)
                                            .With(v => v.WageType, (int)WageType.ApprenticeshipMinimum)
                                            .With(v => v.MinimumWageRate, minApprenticeshipWageRate)
                                            .With(v => v.HoursPerWeek, hoursPerWeek)
                                            .Without(v => v.WageUnitId)
                                            .Create()
                });

            var result = await _sut.GetApprenticeshipVacancyDetailsAsync(VacancyReference);

            result.VacancyReference.Should().Be(VacancyReference);
            result.WageUnit.Should().BeNull();
            result.WageText.Should().Be("£121.50");
        }

        [Test]
        public async Task ShouldHaveUnknownWageForVacancyWithUndefinedHoursPerWeek()
        {
            const decimal minApprenticeshipWageRate = 4.05m;

            _mockMediator.Setup(m => m.Send(It.IsAny<GetApprenticeshipVacancyRequest>(), CancellationToken.None))
                .ReturnsAsync(new GetApprenticeshipVacancyResponse
                {
                    ApprenticeshipVacancy = new Fixture().Build<Domain.Entities.ApprenticeshipVacancy>()
                                            .With(v => v.VacancyReferenceNumber, VacancyReference)
                                            .With(v => v.VacancyStatusId, LiveVacancyStatusId)
                                            .With(v => v.VacancyTypeId, (int)VacancyType.Apprenticeship)
                                            .With(v => v.WageType, (int)WageType.ApprenticeshipMinimum)
                                            .With(v => v.MinimumWageRate, minApprenticeshipWageRate)
                                            .Without(v => v.HoursPerWeek)
                                            .Without(v => v.WageUnitId)
                                            .Create()
                });

            var result = await _sut.GetApprenticeshipVacancyDetailsAsync(VacancyReference);

            result.VacancyReference.Should().Be(VacancyReference);
            result.WageUnit.Should().BeNull();
            result.WageText.Should().Be(UnknownwWageText);
        }
    }
}
