using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Domain.Enum;
using MediatR;

namespace Badil.Application.Features.WasteListing.Commands
{
    public class UpdateWasteListingCommand : IRequest
    {
        public Guid Id { get; set; }
        public string MaterialType { get; set; }
        public double Quantity { get; set; }
        public string Description { get; set; }
        public List<string> ImageUrls { get; set; } = new();
        public decimal SuggestedPrice { get; set; }
        public ListingStatus Status { get; set; }
    }

    public class UpdateWasteListingCommandHandler : IRequestHandler<UpdateWasteListingCommand>
    {
        private readonly IWasteListingRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateWasteListingCommandHandler(IWasteListingRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task Handle(UpdateWasteListingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Waste listing not found");

            if (entity.UserId != _currentUserService.UserId && !_currentUserService.IsAdmin)
                throw new UnauthorizedAccessException("You are not allowed to modify this listing.");

            entity.MaterialType = request.MaterialType;
            entity.Quantity = request.Quantity;
            entity.Description = request.Description;
            entity.ImageUrls = request.ImageUrls;
            entity.SuggestedPrice = request.SuggestedPrice;
            entity.Status = request.Status;

            await _repository.UpdateAsync(entity, cancellationToken);
        }
    }
}
