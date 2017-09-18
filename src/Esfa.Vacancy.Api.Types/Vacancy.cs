﻿using System;

namespace Esfa.Vacancy.Api.Types
{
    /// <summary>
    /// A vacancy for either an apprenticeship or a traineeship 
    /// </summary>
    public class Vacancy
    {
        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        public int VacancyReference { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the short description.
        /// </summary>
        /// <value>
        /// The short description.
        /// </value>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type of the vacancy.
        /// </summary>
        /// <value>
        /// The type of the vacancy.
        /// </value>
        public VacancyType VacancyType { get; set; }

        /// <summary>
        /// Gets or sets the wage unit.
        /// </summary>
        /// <value>
        /// The wage unit.
        /// </value>
        public WageUnit? WageUnit { get; set; }
        
        /// <summary>
        /// Gets or sets the working week.
        /// </summary>
        /// <value>
        /// The working week.
        /// </value>
        public string WorkingWeek { get; set; }

        /// <summary>
        /// Gets or sets the wage text.
        /// </summary>
        /// <value>
        /// The wage text.
        /// </value>
        public string Wage { get; set; }

        /// <summary>
        /// Gets or sets the hours per week.
        /// </summary>
        /// <value>
        /// The hours per week.
        /// </value>
        public decimal? HoursPerWeek { get; set; }

        /// <summary>
        /// Gets or sets the expected duration.
        /// </summary>
        /// <value>
        /// The expected duration.
        /// </value>
        public string ExpectedDuration { get; set; }

        /// <summary>
        /// Gets or sets the expected start date.
        /// </summary>
        /// <value>
        /// The expected start date.
        /// </value>
        public DateTime? ExpectedStartDate { get; set; }

        /// <summary>
        /// Gets or sets the date posted.
        /// </summary>
        /// <value>
        /// The date posted.
        /// </value>
        public DateTime? DatePosted { get; set; }

        /// <summary>
        /// Gets or sets the application closing date.
        /// </summary>
        /// <value>
        /// The application closing date.
        /// </value>
        public DateTime? ApplicationClosingDate { get; set; }

        /// <summary>
        /// Gets or sets the interview from date.
        /// </summary>
        /// <value>
        /// The interview from date.
        /// </value>
        public DateTime? InterviewFromDate { get; set; }

        /// <summary>
        /// Gets or sets the number of positions.
        /// </summary>
        /// <value>
        /// The number of positions.
        /// </value>
        public int NumberOfPositions { get; set; }

        /// <summary>
        /// Training Type
        /// </summary>
        public TrainingType TrainingType { get; set; }

        /// <summary>
        /// Training title.
        /// </summary>
        public string TrainingTitle { get; set; }

        /// <summary>
        /// Training identifier
        /// </summary>
        public string TrainingCode { get; set; }

        /// <summary>
        /// URL to get Training details
        /// </summary>
        public string TrainingUri { get; set; }

        /// <summary>
        /// Employer'e name.
        /// </summary>
        public string EmployerName { get; set; }

        /// <summary>
        /// Employer's description.
        /// </summary>
        public string EmployerDescription { get; set; }

        /// <summary>
        /// Gets or sets the employers website.
        /// </summary>
        /// <value>
        /// The employers website.
        /// </value>
        public string EmployerWebsite { get; set; }

        /// <summary>
        /// Gets or sets the training to be provided.
        /// </summary>
        /// <value>
        /// The training to be provided.
        /// </value>
        public string TrainingToBeProvided { get; set; }

        /// <summary>
        /// Gets or sets the qulificatios required.
        /// </summary>
        /// <value>
        /// The qulificatios required.
        /// </value>
        public string QualificationsRequired { get; set; }

        /// <summary>
        /// Gets or sets the skills required.
        /// </summary>
        /// <value>
        /// The skills required.
        /// </value>
        public string SkillsRequired { get; set; }

        /// <summary>
        /// Gets or sets the personal qualities.
        /// </summary>
        /// <value>
        /// The personal qualities.
        /// </value>
        public string PersonalQualities { get; set; }

        /// <summary>
        /// Gets or sets the important information.
        /// </summary>
        /// <value>
        /// The important information.
        /// </value>
        public string ImportantInformation { get; set; }

        /// <summary>
        /// Gets or sets the future prospects.
        /// </summary>
        /// <value>
        /// The future prospects.
        /// </value>
        public string FutureProspects { get; set; }

        /// <summary>
        /// Gets or sets the things to consider.
        /// </summary>
        /// <value>
        /// The things to consider.
        /// </value>
        public string ThingsToConsider { get; set; }

        /// <summary>
        /// Gets or sets the type of the vacancy location.
        /// </summary>
        /// <value>
        /// The type of the vacancy location.
        /// </value>
        public VacancyLocationType LocationType { get; set; }

        /// <summary>
        /// Gets or sets the supplementary question1.
        /// </summary>
        /// <value>
        /// The supplementary question1.
        /// </value>
        public string SupplementaryQuestion1 { get; set; }

        /// <summary>
        /// Gets or sets the supplementary question2.
        /// </summary>
        /// <value>
        /// The supplementary question2.
        /// </value>
        public string SupplementaryQuestion2 { get; set; }

        /// <summary>
        /// Gets or sets the vacancy URL.
        /// </summary>
        /// <value>
        /// The vacancy URL.
        /// </value>
        public string VacancyUrl { get; set; }

        /// <summary>
        /// Gets or sets the vacancy location.
        /// </summary>
        /// <value>
        /// The vacancy location.
        /// </value>
        public Address Location { get; set; }
    }
}
