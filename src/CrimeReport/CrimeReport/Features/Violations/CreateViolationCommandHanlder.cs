using System;
using CrimeReport.Data;
using MediatR;

namespace CrimeReport.Features.Violations
{
    public class CreateViolationCommandHanlder: IRequestHandler<CreateViolationCommand, Violation>
    {
        private readonly ICreateOrUpdateViolationRepository _repository;
        public CreateViolationCommandHanlder(ICreateOrUpdateViolationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Violation> Handle(CreateViolationCommand request, CancellationToken cancellationToken)
        {
            return await _repository.AddAsync(request.Violation, cancellationToken);
        }
    }
}

