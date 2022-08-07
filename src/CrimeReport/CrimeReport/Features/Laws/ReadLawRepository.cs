using System;
using CrimeReport.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching;
using Microsoft.Extensions.Caching.Distributed;
using CrimeReport.Extensions;

namespace CrimeReport.Features.Laws
{
    public class ReadLawRepository: IReadLawRepository
    {
        private readonly CrimeDbContext _dbContext;
        private readonly IDistributedCache _cache;
        private readonly ILogger<ReadLawRepository> _logger;
        private readonly IConfiguration _configuration;
        public ReadLawRepository(CrimeDbContext dbContext
            , IDistributedCache cache
            , ILogger<ReadLawRepository> logger
            , IConfiguration configuration)
        {
            _dbContext = dbContext;
            _cache = cache;
            _logger = logger;
            _configuration = configuration;
            if (string.IsNullOrWhiteSpace(_configuration["Redis:Keys:LawKey"]))
            {
                throw new ArgumentException("Law Key Configuration Missing");
            }
        }

        public async Task<IList<Law>> GetAllAsync(bool useCache = true, CancellationToken cancellationToken = default)
        {
            if(!useCache)
            {
                return await GetLawsAsync(cancellationToken);
            }
            
            string key = _configuration["Redis:Keys:LawKey"];

            (bool accessed, IList<Law> cachedLaws, string errorMessage) = await _cache.TryGetValuesAsync<IList<Law>>(keyId: key);
            if (!accessed && !string.IsNullOrWhiteSpace(errorMessage))
            {
                _logger.LogError("An error occurred: {error}", errorMessage);
                return await GetLawsAsync(cancellationToken);
            }

            IList<Law> laws;
            (bool saved, string errormessage) setCacheResult = default;
            if (cachedLaws is null || !cachedLaws.Any())
            {
                laws = await GetLawsAsync(cancellationToken);
                setCacheResult = await _cache.TrySetValuesAsync(keyId: key, data: laws);
            }
            else
            {
                laws = cachedLaws;
            }

            if(!setCacheResult.saved && !string.IsNullOrWhiteSpace(setCacheResult.errormessage))
            {
                _logger.LogError("An error occurred: {error}", setCacheResult.errormessage);
            }
            
            return laws;
        }

        private async Task<IList<Law>> GetLawsAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Laws.ToListAsync(cancellationToken: cancellationToken);
        }
    }
}

