using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.MaterialRequest.DTOs;
using MediatR;

namespace Badil.Application.Features.MaterialRequest.Queries
{
    public class GetAllMaterialRequestsQuery : IRequest<List<MaterialRequestDto>>
    {
    }

    public class GetAllMaterialRequestsQueryHandler : IRequestHandler<GetAllMaterialRequestsQuery, List<MaterialRequestDto>>
    {
        private readonly IMaterialRequestRepository _repository;
        private readonly IMapper _mapper;

        public GetAllMaterialRequestsQueryHandler(IMaterialRequestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<MaterialRequestDto>> Handle(GetAllMaterialRequestsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<MaterialRequestDto>>(entities);
        }
    }
}
