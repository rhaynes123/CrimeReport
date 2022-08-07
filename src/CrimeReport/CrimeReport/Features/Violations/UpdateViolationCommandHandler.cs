using System;
using MediatR;

namespace CrimeReport.Features.Violations
{
    public class UpdateViolationCommandHandler : IRequestHandler<UpdateViolationCommand, Violation>
    {
        private readonly ICreateOrUpdateViolationRepository _repository;
        public UpdateViolationCommandHandler(ICreateOrUpdateViolationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Violation> Handle(UpdateViolationCommand request, CancellationToken cancellationToken)
        {
            return await _repository.ModifyAsync(request.Violation, cancellationToken: cancellationToken);
        }
    }
}

