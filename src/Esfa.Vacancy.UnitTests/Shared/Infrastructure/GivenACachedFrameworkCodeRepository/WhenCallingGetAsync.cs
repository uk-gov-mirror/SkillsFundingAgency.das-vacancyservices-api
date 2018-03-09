﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Esfa.Vacancy.Domain.Interfaces;
using Esfa.Vacancy.Domain.Repositories;
using Esfa.Vacancy.Infrastructure.Repositories;
using Moq;
using NUnit.Framework;
using SFA.DAS.NLog.Logger;

namespace Esfa.Vacancy.UnitTests.Shared.Infrastructure.GivenACachedFrameworkCodeRepository
{
    [TestFixture]
    public class WhenCallingGetAsync
    {
        [Test]
        public async Task ThenShouldUseCache()
        {
            var logger = new Mock<ILog>();
            var cache = new Mock<ICacheService>();

            cache.Setup(c => c.CacheAsideAsync(It.IsAny<string>(), It.IsAny<Func<Task<IEnumerable<string>>>>(), It.IsAny<TimeSpan?>()))
                .Returns((string key, Func<Task<IEnumerable<string>>> action, TimeSpan timeSpan) => action());

            var respository = new Mock<IFrameworkCodeRepository>();
            respository.Setup(r => r.GetAsync()).Returns(Task.FromResult(Enumerable.Empty<string>()));

            var cachedRepository = new CachedFrameworkCodeRepository(respository.Object, logger.Object, cache.Object);

            await cachedRepository.GetAsync();

            cache.Verify(c => c.CacheAsideAsync(It.IsAny<string>(), It.IsAny<Func<Task<IEnumerable<string>>>>(), It.IsAny<TimeSpan?>()), Times.Once);
        }
    }
}
