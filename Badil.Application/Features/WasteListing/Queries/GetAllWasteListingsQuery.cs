using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.WasteListing.DTOs;
using MediatR;

namespace Badil.Application.Features.WasteListing.Queries
{
    public class GetAllWasteListingsQuery : IRequest<List<WasteListingDto>>
    {
    }

    public class GetAllWasteListingsQueryHandler : IRequestHandler<GetAllWasteListingsQuery, List<WasteListingDto>>
    {
        private readonly IWasteListingRepository _repository;
        private readonly IMapper _mapper;

        public GetAllWasteListingsQueryHandler(IWasteListingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<WasteListingDto>> Handle(GetAllWasteListingsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<WasteListingDto>>(entities);
        }
    }
}
