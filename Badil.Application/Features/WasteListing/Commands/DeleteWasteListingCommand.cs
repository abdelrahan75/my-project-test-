using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using MediatR;

namespace Badil.Application.Features.WasteListing.Commands
{
    public class DeleteWasteListingCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteWasteListingCommandHandler : IRequestHandler<DeleteWasteListingCommand>
    {
        private readonly IWasteListingRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteWasteListingCommandHandler(IWasteListingRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task Handle(DeleteWasteListingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Waste listing not found");

            if (entity.UserId != _currentUserService.UserId && !_currentUserService.IsAdmin)
                throw new UnauthorizedAccessException("You are not allowed to delete this listing.");

            await _repository.DeleteAsync(entity, cancellationToken);
        }
    }
}
