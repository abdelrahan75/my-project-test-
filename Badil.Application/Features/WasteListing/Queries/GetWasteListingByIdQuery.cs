using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.WasteListing.DTOs;
using MediatR;

namespace Badil.Application.Features.WasteListing.Queries
{
    public class GetWasteListingByIdQuery : IRequest<WasteListingDto>
    {
        public Guid Id { get; set; }
    }

    public class GetWasteListingByIdQueryHandler : IRequestHandler<GetWasteListingByIdQuery, WasteListingDto>
    {
        private readonly IWasteListingRepository _repository;
        private readonly IMapper _mapper;

        public GetWasteListingByIdQueryHandler(IWasteListingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<WasteListingDto> Handle(GetWasteListingByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Waste listing not found");

            return _mapper.Map<WasteListingDto>(entity);
        }
    }
}
