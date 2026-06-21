using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.ResourceMatch.DTOs;
using MediatR;

namespace Badil.Application.Features.ResourceMatch.Queries
{
    public class GetAllResourceMatchesQuery : IRequest<List<ResourceMatchDto>>
    {
    }

    public class GetAllResourceMatchesQueryHandler : IRequestHandler<GetAllResourceMatchesQuery, List<ResourceMatchDto>>
    {
        private readonly IResourceMatchRepository _repository;
        private readonly IMapper _mapper;

        public GetAllResourceMatchesQueryHandler(IResourceMatchRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ResourceMatchDto>> Handle(GetAllResourceMatchesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<ResourceMatchDto>>(entities);
        }
    }
}
