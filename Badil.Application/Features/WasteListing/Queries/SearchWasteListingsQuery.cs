using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.WasteListing.DTOs;
using MediatR;

namespace Badil.Application.Features.WasteListing.Queries
{
    public class SearchWasteListingsQuery : IRequest<List<WasteListingDto>>
    {
        public string? MaterialType { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? Status { get; set; }
    }

    public class SearchWasteListingsQueryHandler : IRequestHandler<SearchWasteListingsQuery, List<WasteListingDto>>
    {
        private readonly IWasteListingRepository _repository;
        private readonly IMapper _mapper;

        public SearchWasteListingsQueryHandler(IWasteListingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<WasteListingDto>> Handle(SearchWasteListingsQuery request, CancellationToken cancellationToken)
        {
            var all = await _repository.GetAllAsync(cancellationToken);
            var filtered = all.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(request.MaterialType))
                filtered = filtered.Where(l => l.MaterialType.Contains(request.MaterialType, StringComparison.OrdinalIgnoreCase));
            if (request.MinPrice.HasValue)
                filtered = filtered.Where(l => l.SuggestedPrice >= request.MinPrice.Value);
            if (request.MaxPrice.HasValue)
                filtered = filtered.Where(l => l.SuggestedPrice <= request.MaxPrice.Value);
            if (!string.IsNullOrWhiteSpace(request.Status) && Enum.TryParse<Domain.Enum.ListingStatus>(request.Status, true, out var status))
                filtered = filtered.Where(l => l.Status == status);

            return _mapper.Map<List<WasteListingDto>>(filtered.ToList());
        }
    }
}
