using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.ResourceMatch.DTOs;
using MediatR;

namespace Badil.Application.Features.ResourceMatch.Queries
{
    public class GetMatchesForListingQuery : IRequest<List<ResourceMatchDto>>
    {
        public Guid ListingId { get; set; }
    }

    public class GetMatchesForListingQueryHandler : IRequestHandler<GetMatchesForListingQuery, List<ResourceMatchDto>>
    {
        private readonly IResourceMatchRepository _repository;
        private readonly IMapper _mapper;

        public GetMatchesForListingQueryHandler(IResourceMatchRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ResourceMatchDto>> Handle(GetMatchesForListingQuery request, CancellationToken cancellationToken)
        {
            var all = await _repository.GetAllAsync(cancellationToken);
            var matches = all.Where(m => m.ListingId == request.ListingId).ToList();
            return _mapper.Map<List<ResourceMatchDto>>(matches);
        }
    }
}
