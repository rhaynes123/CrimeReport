using System;
using CrimeReport.Data;
using MediatR;

namespace CrimeReport.Features.Crimes
{
    public class CreateCrimeReportCommandHandler: IRequestHandler<CreateCrimeReportCommand, Crime>
    {
        private readonly CrimeDbContext _dbContext;
        public CreateCrimeReportCommandHandler(CrimeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Crime> Handle(CreateCrimeReportCommand request, CancellationToken cancellationToken)
        {
            var crimeReportEntry = await _dbContext.AddAsync(request.Crime, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return crimeReportEntry.Entity;
        }
    }
}

