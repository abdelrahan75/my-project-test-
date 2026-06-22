using AutoMapper;
using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.WasteListing.DTOs;
using MediatR;

namespace Badil.Application.Features.WasteListing.Queries
{
    public class GetMyWasteListingsQuery : IRequest<List<WasteListingDto>>
    {
    }

    public class GetMyWasteListingsQueryHandler : IRequestHandler<GetMyWasteListingsQuery, List<WasteListingDto>>
    {
        private readonly IWasteListingRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetMyWasteListingsQueryHandler(IWasteListingRepository repository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<List<WasteListingDto>> Handle(GetMyWasteListingsQuery request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId is null)
                throw new UnauthorizedAccessException("User is not authenticated.");
            var all = await _repository.GetAllAsync(cancellationToken);
            var mine = all.Where(l => l.UserId == _currentUserService.UserId).ToList();
            return _mapper.Map<List<WasteListingDto>>(mine);
        }
    }
}
