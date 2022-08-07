using System;
namespace CrimeReport.Features.Laws
{
    public interface ICreateOrUpdateLawsRepository
    {
        Task<Law> AddAsync(Law law, CancellationToken cancellationToken = default);
    }
}

