using System;
using CrimeReport.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CrimeReport.Features.Violations
{
    public class GetSingleViolationQueryHandler :IRequestHandler<GetSingleViolationQuery, Violation>
    {
        private readonly CrimeDbContext _dbContext;
        public GetSingleViolationQueryHandler(CrimeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Violation> Handle(GetSingleViolationQuery request, CancellationToken cancellationToken)
        {
            if(request.TypeOfCrime == default)
            {
                //Using FirstOrDefault Here over single because single will read through an entire data store and THEN return the single value
                //First will read through but stop once it finds the first thing its looking for.
                //Since there is no partition key I want this as fast as possible. https://www.youtube.com/watch?v=ZTWl2s8ScMc
                return await _dbContext.Violations
                    .FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken) ?? default!;
            }
            return await _dbContext.Violations
                .WithPartitionKey(request.TypeOfCrime.ToString())
                .SingleOrDefaultAsync(v=>v.Id == request.Id, cancellationToken: cancellationToken) ?? default!;
        }
    }
}

