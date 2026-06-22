using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using MediatR;

namespace Badil.Application.Features.Notification.Commands
{
    public class MarkAllNotificationsReadCommand : IRequest
    {
    }

    public class MarkAllNotificationsReadCommandHandler : IRequestHandler<MarkAllNotificationsReadCommand>
    {
        private readonly INotificationRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public MarkAllNotificationsReadCommandHandler(INotificationRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task Handle(MarkAllNotificationsReadCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId is null)
                throw new UnauthorizedAccessException("User is not authenticated.");
            var all = await _repository.GetAllAsync(cancellationToken);
            var mine = all.Where(n => n.UserId == _currentUserService.UserId && !n.IsRead).ToList();
            foreach (var n in mine)
            {
                n.IsRead = true;
                await _repository.UpdateAsync(n, cancellationToken);
            }
        }
    }
}
