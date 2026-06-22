using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Domain.Enum;
using MediatR;

namespace Badil.Application.Features.VerificationRequest.Commands
{
    public class ApproveVerificationRequestCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class ApproveVerificationRequestCommandHandler : IRequestHandler<ApproveVerificationRequestCommand>
    {
        private readonly IVerificationRequestRepository _repository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICurrentUserService _currentUserService;

        public ApproveVerificationRequestCommandHandler(
            IVerificationRequestRepository repository,
            ICompanyRepository companyRepository,
            ICurrentUserService currentUserService)
        {
            _repository = repository;
            _companyRepository = companyRepository;
            _currentUserService = currentUserService;
        }

        public async Task Handle(ApproveVerificationRequestCommand request, CancellationToken cancellationToken)
        {
            var verifRequest = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (verifRequest == null)
                throw new KeyNotFoundException("Verification request not found");

            verifRequest.Status = VerificationStatus.Approved;
            verifRequest.ReviewedByAdminId = _currentUserService.UserId;
            await _repository.UpdateAsync(verifRequest, cancellationToken);

            var company = await _companyRepository.GetByIdAsync(verifRequest.CompanyId, cancellationToken);
            if (company != null)
            {
                company.IsVerified = true;
                await _companyRepository.UpdateAsync(company, cancellationToken);
            }
        }
    }
}
