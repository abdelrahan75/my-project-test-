using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Domain.Entity;
using MediatR;

namespace Badil.Application.Features.Company.Commands
{
    public class UpdateCompanyCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Sector { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string TaxRegistrationNumber { get; set; }
        public string CommercialRegisterNumber { get; set; }
    }

    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateCompanyCommandHandler(ICompanyRepository companyRepository, ICurrentUserService currentUserService)
        {
            _companyRepository = companyRepository;
            _currentUserService = currentUserService;
        }

        public async Task Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _companyRepository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Company not found");

            if (entity.UserId != _currentUserService.UserId && !_currentUserService.IsAdmin)
                throw new UnauthorizedAccessException("You are not allowed to modify this company.");

            entity.Name = request.Name;
            entity.Sector = request.Sector;
            entity.Address = request.Address;
            entity.Location = new GeoLocation(request.Latitude, request.Longitude);
            entity.TaxRegistrationNumber = request.TaxRegistrationNumber;
            entity.CommercialRegisterNumber = request.CommercialRegisterNumber;

            await _companyRepository.UpdateAsync(entity, cancellationToken);
        }
    }
}
