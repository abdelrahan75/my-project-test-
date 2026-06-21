using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.Notification.DTOs;
using MediatR;

namespace Badil.Application.Features.Notification.Queries
{
    public class GetNotificationByIdQuery : IRequest<NotificationDto>
    {
        public Guid Id { get; set; }
    }

    public class GetNotificationByIdQueryHandler : IRequestHandler<GetNotificationByIdQuery, NotificationDto>
    {
        private readonly INotificationRepository _repository;
        private readonly IMapper _mapper;

        public GetNotificationByIdQueryHandler(INotificationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<NotificationDto> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Notification not found");

            return _mapper.Map<NotificationDto>(entity);
        }
    }
}
