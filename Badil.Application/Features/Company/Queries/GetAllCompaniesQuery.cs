using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.Company.DTOs;
using MediatR;

namespace Badil.Application.Features.Company.Queries
{
    public class GetAllCompaniesQuery : IRequest<List<CompanyDto>>
    {
    }

    public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, List<CompanyDto>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public GetAllCompaniesQueryHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<List<CompanyDto>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _companyRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<CompanyDto>>(entities);
        }
    }
}
