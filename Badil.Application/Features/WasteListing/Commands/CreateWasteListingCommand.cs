using AutoMapper;
using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.WasteListing.DTOs;
using Badil.Domain.Enum;
using MediatR;

namespace Badil.Application.Features.WasteListing.Commands
{
    public class CreateWasteListingCommand : IRequest<WasteListingDto>
    {
        public string MaterialType { get; set; }
        public double Quantity { get; set; }
        public string Description { get; set; }
        public List<string> ImageUrls { get; set; } = new();
        public decimal SuggestedPrice { get; set; }
    }

    public class CreateWasteListingCommandHandler : IRequestHandler<CreateWasteListingCommand, WasteListingDto>
    {
        private readonly IWasteListingRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public CreateWasteListingCommandHandler(IWasteListingRepository repository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<WasteListingDto> Handle(CreateWasteListingCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId is null)
                throw new UnauthorizedAccessException("User is not authenticated.");

            var entity = new Badil.Domain.Entity.WasteListing
            {
                UserId = _currentUserService.UserId.Value,
                MaterialType = request.MaterialType,
                Quantity = request.Quantity,
                Description = request.Description,
                ImageUrls = request.ImageUrls,
                SuggestedPrice = request.SuggestedPrice,
                AIStandardizedTag = string.Empty,
                IsVisuallyValidated = false,
                Status = ListingStatus.Draft
            };

            await _repository.AddAsync(entity, cancellationToken);

            return _mapper.Map<WasteListingDto>(entity);
        }
    }
}
