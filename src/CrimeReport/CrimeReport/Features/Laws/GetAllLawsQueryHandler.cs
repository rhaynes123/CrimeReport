using System;
using CrimeReport.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CrimeReport.Features.Laws
{
    public class GetAllLawsQueryHandler: IRequestHandler<GetAllLawsQuery, IList<Law>>
    {
        private readonly IReadLawRepository _readLawRepository;
        public GetAllLawsQueryHandler(IReadLawRepository readLawRepository)
        {
            _readLawRepository = readLawRepository;
        }

        public async Task<IList<Law>> Handle(GetAllLawsQuery request, CancellationToken cancellationToken)
        {
            return await _readLawRepository.GetAllAsync(cancellationToken:cancellationToken);
        }
    }
}

