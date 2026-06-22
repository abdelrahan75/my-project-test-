using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using MediatR;

namespace Badil.Application.Features.Notification.Queries
{
    public class GetUnreadNotificationCountQuery : IRequest<int>
    {
    }

    public class GetUnreadNotificationCountQueryHandler : IRequestHandler<GetUnreadNotificationCountQuery, int>
    {
        private readonly INotificationRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public GetUnreadNotificationCountQueryHandler(INotificationRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(GetUnreadNotificationCountQuery request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId is null)
                return 0;
            var all = await _repository.GetAllAsync(cancellationToken);
            return all.Count(n => n.UserId == _currentUserService.UserId && !n.IsRead);
        }
    }
}
