using System;
using Microsoft.EntityFrameworkCore;
using CrimeReport.Data;
using MediatR;

namespace CrimeReport.Features.Laws
{
    public class GetSingleLawQueryHandler: IRequestHandler<GetSingleLawQuery, Law>
    {
        private readonly CrimeDbContext _dbContext;
        public GetSingleLawQueryHandler(CrimeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Law> Handle(GetSingleLawQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Laws
                .WithPartitionKey(request.Id)
                .SingleOrDefaultAsync(l =>l.Id == request.Id, cancellationToken: cancellationToken) ?? default!;
        }
    }
}

