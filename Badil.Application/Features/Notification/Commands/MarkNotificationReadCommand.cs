using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using MediatR;

namespace Badil.Application.Features.Notification.Commands
{
    public class MarkNotificationReadCommand : IRequest
    {
        public Guid NotificationId { get; set; }
    }

    public class MarkNotificationReadCommandHandler : IRequestHandler<MarkNotificationReadCommand>
    {
        private readonly INotificationRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public MarkNotificationReadCommandHandler(INotificationRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task Handle(MarkNotificationReadCommand request, CancellationToken cancellationToken)
        {
            var notification = await _repository.GetByIdAsync(request.NotificationId, cancellationToken);
            if (notification == null)
                throw new KeyNotFoundException("Notification not found");
            if (notification.UserId != _currentUserService.UserId)
                throw new UnauthorizedAccessException("Access denied");
            notification.IsRead = true;
            await _repository.UpdateAsync(notification, cancellationToken);
        }
    }
}
