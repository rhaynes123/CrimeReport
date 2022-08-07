using System;
using CrimeReport.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CrimeReport.Features.Violations
{
    public class CreateOrUpdateViolationRepository: ICreateOrUpdateViolationRepository
    {
        private readonly CrimeDbContext _context;
        public CreateOrUpdateViolationRepository(CrimeDbContext context)
        {
            _context = context;
        }
        private bool ViolationExists(string id)
        {
            return _context.Violations.Any(e => e.Id == id);
        }

        public async Task<Violation> AddAsync(Violation violation, CancellationToken cancellationToken)
        {
            EntityEntry<Violation>? violationEntity = await _context.AddAsync(violation, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return violationEntity.Entity;
        }



        public async Task<Violation> ModifyAsync(Violation violation, CancellationToken cancellationToken)
        {
            var modifiedEntity = _context.Attach(violation).State = EntityState.Modified;

            try
            {
                int saved =  await _context.SaveChangesAsync();
                return violation;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ViolationExists(violation.Id))
                {
                    return default!;
                }
                else
                {
                    throw;
                }
            }
        }
    }
}

