using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Domain.Enum;
using MediatR;

namespace Badil.Application.Features.VerificationRequest.Commands
{
    public class RejectVerificationRequestCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Reason { get; set; }
    }

    public class RejectVerificationRequestCommandHandler : IRequestHandler<RejectVerificationRequestCommand>
    {
        private readonly IVerificationRequestRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public RejectVerificationRequestCommandHandler(
            IVerificationRequestRepository repository,
            ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task Handle(RejectVerificationRequestCommand request, CancellationToken cancellationToken)
        {
            var verifRequest = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (verifRequest == null)
                throw new KeyNotFoundException("Verification request not found");

            verifRequest.Status = VerificationStatus.Rejected;
            verifRequest.ReviewedByAdminId = _currentUserService.UserId;
            await _repository.UpdateAsync(verifRequest, cancellationToken);
        }
    }
}
