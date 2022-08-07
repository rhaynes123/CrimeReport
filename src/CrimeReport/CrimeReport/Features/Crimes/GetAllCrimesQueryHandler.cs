using System;
using CrimeReport.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CrimeReport.Features.Crimes
{
    public class GetAllCrimesQueryHandler : IRequestHandler<GetAllCrimesQuery, IList<Crime>>
    {
        private readonly CrimeDbContext _dbContext;
        public GetAllCrimesQueryHandler(CrimeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<Crime>> Handle(GetAllCrimesQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Crimes.ToListAsync(cancellationToken: cancellationToken);
        }
    }
}

