using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CrimeReport.Features.Violations
{
    public interface ICreateOrUpdateViolationRepository
    {
        Task<Violation> AddAsync(Violation violation, CancellationToken cancellationToken = default);
        Task<Violation> ModifyAsync(Violation violation, CancellationToken cancellationToken = default);
    }
}

