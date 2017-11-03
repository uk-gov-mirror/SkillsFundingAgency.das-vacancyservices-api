﻿using System;
using System.Collections.Generic;

namespace Esfa.Vacancy.Register.Domain.Entities
{
    public class VacancySearchParameters
    {
        public List<string> FrameworkLarsCodes { get; set; } = new List<string>();
        public List<string> StandardLarsCodes { get; set; } = new List<string>();
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public DateTime? FromDate { get; set; }
        public string LocationType { get; set; }
    }
}
