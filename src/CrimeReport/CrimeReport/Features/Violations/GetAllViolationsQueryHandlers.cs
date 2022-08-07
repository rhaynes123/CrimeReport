using System;
using CrimeReport.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CrimeReport.Features.Violations
{
    public class GetAllViolationsQueryHandlers: IRequestHandler<GetAllViolationsQuery, IEnumerable<Violation>>
    {
        private readonly CrimeDbContext _dbContext;
        public GetAllViolationsQueryHandlers(CrimeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Violation>> Handle(GetAllViolationsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Violations
                .WithPartitionKey(request.TypeOfCrime.ToString())
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}

