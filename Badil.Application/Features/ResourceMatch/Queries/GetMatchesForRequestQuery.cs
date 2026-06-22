using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.ResourceMatch.DTOs;
using MediatR;

namespace Badil.Application.Features.ResourceMatch.Queries
{
    public class GetMatchesForRequestQuery : IRequest<List<ResourceMatchDto>>
    {
        public Guid RequestId { get; set; }
    }

    public class GetMatchesForRequestQueryHandler : IRequestHandler<GetMatchesForRequestQuery, List<ResourceMatchDto>>
    {
        private readonly IResourceMatchRepository _repository;
        private readonly IMapper _mapper;

        public GetMatchesForRequestQueryHandler(IResourceMatchRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ResourceMatchDto>> Handle(GetMatchesForRequestQuery request, CancellationToken cancellationToken)
        {
            var all = await _repository.GetAllAsync(cancellationToken);
            var matches = all.Where(m => m.RequestId == request.RequestId).ToList();
            return _mapper.Map<List<ResourceMatchDto>>(matches);
        }
    }
}
