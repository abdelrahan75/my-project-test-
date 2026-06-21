using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using MediatR;

namespace Badil.Application.Features.Company.Commands
{
    public class DeleteCompanyCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteCompanyCommandHandler(ICompanyRepository companyRepository, ICurrentUserService currentUserService)
        {
            _companyRepository = companyRepository;
            _currentUserService = currentUserService;
        }

        public async Task Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _companyRepository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Company not found");

            if (entity.UserId != _currentUserService.UserId && !_currentUserService.IsAdmin)
                throw new UnauthorizedAccessException("You are not allowed to delete this company.");

            await _companyRepository.DeleteAsync(entity, cancellationToken);
        }
    }
}
