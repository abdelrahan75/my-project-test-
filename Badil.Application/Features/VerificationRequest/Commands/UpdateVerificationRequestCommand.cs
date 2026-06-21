using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Domain.Enum;
using MediatR;

namespace Badil.Application.Features.VerificationRequest.Commands
{
    public class UpdateVerificationRequestCommand : IRequest
    {
        public Guid Id { get; set; }
        public VerificationStatus Status { get; set; }
    }

    public class UpdateVerificationRequestCommandHandler : IRequestHandler<UpdateVerificationRequestCommand>
    {
        private readonly IVerificationRequestRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateVerificationRequestCommandHandler(IVerificationRequestRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task Handle(UpdateVerificationRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Verification request not found");

            entity.Status = request.Status;
            entity.ReviewedByAdminId = _currentUserService.UserId;

            await _repository.UpdateAsync(entity, cancellationToken);
        }
    }
}
