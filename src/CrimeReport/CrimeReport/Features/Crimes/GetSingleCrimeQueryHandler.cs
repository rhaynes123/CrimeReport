using System;
using CrimeReport.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CrimeReport.Features.Crimes
{
    public class GetSingleCrimeQueryHandler: IRequestHandler<GetSingleCrimeQuery, Crime>
    {
        private readonly CrimeDbContext _dbContext;
        public GetSingleCrimeQueryHandler(CrimeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Crime> Handle(GetSingleCrimeQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Crimes
                .WithPartitionKey(request.Id)
                .SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken: cancellationToken) ?? default!;
        }
    }
}

