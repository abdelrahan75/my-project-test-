using Badil.Application.Common.Interfaces.Repositories;
using MediatR;

namespace Badil.Application.Features.VerificationRequest.Commands
{
    public class DeleteVerificationRequestCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteVerificationRequestCommandHandler : IRequestHandler<DeleteVerificationRequestCommand>
    {
        private readonly IVerificationRequestRepository _repository;

        public DeleteVerificationRequestCommandHandler(IVerificationRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteVerificationRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Verification request not found");

            await _repository.DeleteAsync(entity, cancellationToken);
        }
    }
}
