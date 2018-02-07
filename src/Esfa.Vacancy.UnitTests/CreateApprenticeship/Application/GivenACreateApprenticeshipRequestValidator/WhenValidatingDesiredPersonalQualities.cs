﻿namespace Esfa.Vacancy.UnitTests.CreateApprenticeship.Application.GivenACreateApprenticeshipRequestValidator
{
    using System;
    using System.Collections.Generic;
    using Domain.Validation;
    using FluentValidation.TestHelper;
    using NUnit.Framework;
    using Vacancy.Application.Commands.CreateApprenticeship;
    using Vacancy.Application.Commands.CreateApprenticeship.Validators;

    [TestFixture]
    public class WhenValidatingDesiredPersonalQualities
    {
        private static List<TestCaseData> TestCases() =>
            new List<TestCaseData>
            {
                new TestCaseData(null, "'Desired Personal Qualities' should not be empty.", true)
                    .SetName("Then DesiredPersonalQualities cannot be null"),
                new TestCaseData("", "'Desired Personal Qualities' should not be empty.", true)
                    .SetName("Then DesiredPersonalQualities cannot be empty"),
                new TestCaseData("desired personal qualities", null, false)
                    .SetName("Then DesiredPersonalQualities is not empty"),
                new TestCaseData(new String('a', 4001),
                        "'Desired Personal Qualities' must be less than 4001 characters. You entered 4001 characters.", true)
                    .SetName("Then DesiredPersonalQualities cannot be more than 4000 characters"),
                new TestCaseData("<", "'Desired Personal Qualities' can't contain invalid characters", true)
                    .SetName("Then DesiredPersonalQualities cannot contain invalid characters"),
                new TestCaseData("<script>", "'Desired Personal Qualities' can't contain blacklisted HTML elements", true)
                    .SetName("Then DesiredPersonalQualities cannot contain blacklisted HTML elements")
            };

        [TestCaseSource(nameof(TestCases))]
        public void ValidateDesiredPersonalQualities(string value, string errorMessage, bool shouldError)
        {
            var request = new CreateApprenticeshipRequest
            {
                DesiredPersonalQualities = value
            };

            var sut = new CreateApprenticeshipRequestValidator();
            if (shouldError)
            {
                sut.Validate(request);
                sut.ShouldHaveValidationErrorFor(req => req.DesiredPersonalQualities, request)
                   .WithErrorCode(ErrorCodes.CreateApprenticeship.DesiredPersonalQualities)
                   .WithErrorMessage(errorMessage);
            }
            else
            {
                sut.ShouldNotHaveValidationErrorFor(req => req.DesiredPersonalQualities, request);
            }
        }
    }
}