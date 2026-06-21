using AutoMapper;
using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.Company.DTOs;
using Badil.Domain.Entity;
using MediatR;

namespace Badil.Application.Features.Company.Commands
{
    public class CreateCompanyCommand : IRequest<CompanyDto>
    {
        public string Name { get; set; }
        public string Sector { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string TaxRegistrationNumber { get; set; }
        public string CommercialRegisterNumber { get; set; }
    }

    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyDto>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public CreateCompanyCommandHandler(ICompanyRepository companyRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId is null)
                throw new UnauthorizedAccessException("User is not authenticated.");

            var entity = new Badil.Domain.Entity.Company
            {
                UserId = _currentUserService.UserId.Value,
                Name = request.Name,
                Sector = request.Sector,
                Address = request.Address,
                Location = new GeoLocation(request.Latitude, request.Longitude),
                TaxRegistrationNumber = request.TaxRegistrationNumber,
                CommercialRegisterNumber = request.CommercialRegisterNumber,
                IsVerified = false
            };

            await _companyRepository.AddAsync(entity, cancellationToken);

            return _mapper.Map<CompanyDto>(entity);
        }
    }
}
