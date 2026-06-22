using AutoMapper;
using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.Notification.DTOs;
using MediatR;

namespace Badil.Application.Features.Notification.Queries
{
    public class GetMyNotificationsQuery : IRequest<List<NotificationDto>>
    {
    }

    public class GetMyNotificationsQueryHandler : IRequestHandler<GetMyNotificationsQuery, List<NotificationDto>>
    {
        private readonly INotificationRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetMyNotificationsQueryHandler(INotificationRepository repository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<List<NotificationDto>> Handle(GetMyNotificationsQuery request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId is null)
                throw new UnauthorizedAccessException("User is not authenticated.");
            var all = await _repository.GetAllAsync(cancellationToken);
            var mine = all.Where(n => n.UserId == _currentUserService.UserId).OrderByDescending(n => n.CreatedAt).ToList();
            return _mapper.Map<List<NotificationDto>>(mine);
        }
    }
}
