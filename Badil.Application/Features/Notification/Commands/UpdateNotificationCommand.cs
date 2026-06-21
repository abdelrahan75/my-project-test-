using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using MediatR;

namespace Badil.Application.Features.Notification.Commands
{
    public class UpdateNotificationCommand : IRequest
    {
        public Guid Id { get; set; }
        public bool IsRead { get; set; }
    }

    public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand>
    {
        private readonly INotificationRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateNotificationCommandHandler(INotificationRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Notification not found");

            if (entity.UserId != _currentUserService.UserId && !_currentUserService.IsAdmin)
                throw new UnauthorizedAccessException("You are not allowed to modify this notification.");

            entity.IsRead = request.IsRead;

            await _repository.UpdateAsync(entity, cancellationToken);
        }
    }
}
