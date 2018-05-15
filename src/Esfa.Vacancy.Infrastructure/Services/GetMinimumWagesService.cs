﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Esfa.Vacancy.Application.Interfaces;
using Esfa.Vacancy.Domain.Entities;
using Esfa.Vacancy.Infrastructure.Exceptions;
using Esfa.Vacancy.Infrastructure.Factories;

namespace Esfa.Vacancy.Infrastructure.Services
{
    public class GetMinimumWagesService : IGetMinimumWagesService
    {
        private readonly SqlDatabaseService _sqlDatabaseService;
        private const string GetAllApprenticeMinimumWagesStoredProc = "[VACANCY_API].[GetWageRanges]";

        public GetMinimumWagesService(SqlDatabaseService sqlDatabaseService)
        {
            _sqlDatabaseService = sqlDatabaseService;
        }

        public async Task<IEnumerable<WageRange>> GetAllWagesAsync()
        {
            try
            {
                var result = await _sqlDatabaseService.ExecuteWithRetryAsync(
                    "Get all Apprentice Minimum Wages",
                    async sqlConn =>
                    {
                        var results = await sqlConn.QueryAsync<WageRange>(
                            GetAllApprenticeMinimumWagesStoredProc,
                            commandType: CommandType.StoredProcedure);

                        return results;
                    });
                return result;
            }
            catch (Exception e)
            {
                throw new InfrastructureException(e);
            }
        }

        public async Task<WageRange> GetWageRangeAsync(DateTime expectedStartDate)
        {
            var ranges = await GetAllWagesAsync();

            return ranges?.FirstOrDefault(range =>
                range.ValidFrom.Date <= expectedStartDate.Date &&
                range.ValidTo.Date >= expectedStartDate.Date);
        }
    }
}