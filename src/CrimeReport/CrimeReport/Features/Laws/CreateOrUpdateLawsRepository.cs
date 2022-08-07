using System;
using CrimeReport.Data;
using CrimeReport.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace CrimeReport.Features.Laws
{
    public class CreateOrUpdateLawsRepository: ICreateOrUpdateLawsRepository
    {
        private readonly CrimeDbContext _context;
        private readonly IDistributedCache _cache;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CreateOrUpdateLawsRepository> _logger;
        public CreateOrUpdateLawsRepository(CrimeDbContext context
            , IDistributedCache cache
            ,IConfiguration configuration
            ,ILogger<CreateOrUpdateLawsRepository> logger)
        {
            _configuration = configuration;
            if (string.IsNullOrWhiteSpace(_configuration["Redis:Keys:LawKey"]))
            {
                throw new ArgumentException("Law Key Configuration Missing");
            }

            _context = context;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Law> AddAsync(Law law, CancellationToken cancellationToken = default)
        {
            _context.Laws.Add(law);
            int changesSaved = await _context.SaveChangesAsync(cancellationToken);
            var laws = await _context.Laws.ToListAsync(cancellationToken);
            string key = _configuration["Redis:Keys:LawKey"];
            (bool saved, string errormessage) = await _cache.TrySetValuesAsync(keyId: key, data: laws);
            if(!saved || !string.IsNullOrWhiteSpace(errormessage))
            {
                _logger.LogWarning("Warning Cache Could not be set {warning}", errormessage);
            }
            return law;
        }
    }
}

