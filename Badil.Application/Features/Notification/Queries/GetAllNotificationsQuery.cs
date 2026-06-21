using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.Notification.DTOs;
using MediatR;

namespace Badil.Application.Features.Notification.Queries
{
    public class GetAllNotificationsQuery : IRequest<List<NotificationDto>>
    {
    }

    public class GetAllNotificationsQueryHandler : IRequestHandler<GetAllNotificationsQuery, List<NotificationDto>>
    {
        private readonly INotificationRepository _repository;
        private readonly IMapper _mapper;

        public GetAllNotificationsQueryHandler(INotificationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<NotificationDto>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<NotificationDto>>(entities);
        }
    }
}
