using System;
using CrimeReport.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CrimeReport.Features.Laws
{
    public class GetAllLawsQueryHandler: IRequestHandler<GetAllLawsQuery, IList<Law>>
    {
        private readonly CrimeDbContext _context;
        public GetAllLawsQueryHandler(CrimeDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Law>> Handle(GetAllLawsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Laws.ToListAsync(cancellationToken);
        }
    }
}

