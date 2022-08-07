using System;
using MediatR;

namespace CrimeReport.Features.Laws
{
    public class CreateLawCommandHandler: IRequestHandler<CreateLawCommand, Law>
    {
        private readonly ICreateOrUpdateLawsRepository _createRepository;
        public CreateLawCommandHandler(ICreateOrUpdateLawsRepository repository)
        {
            _createRepository = repository;
        }

        public async Task<Law> Handle(CreateLawCommand request, CancellationToken cancellationToken)
        {
            return await _createRepository.AddAsync(request.Law, cancellationToken);
        }
    }
}

