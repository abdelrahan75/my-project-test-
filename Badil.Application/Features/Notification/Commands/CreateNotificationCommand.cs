using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.Notification.DTOs;
using Badil.Domain.Enum;
using MediatR;

namespace Badil.Application.Features.Notification.Commands
{
    public class CreateNotificationCommand : IRequest<NotificationDto>
    {
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public NotificationType Type { get; set; }
    }

    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, NotificationDto>
    {
        private readonly INotificationRepository _repository;
        private readonly IMapper _mapper;

        public CreateNotificationCommandHandler(INotificationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<NotificationDto> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var entity = new Badil.Domain.Entity.Notification
            {
                UserId = request.UserId,
                Content = request.Content,
                Type = request.Type,
                IsRead = false
            };

            await _repository.AddAsync(entity, cancellationToken);

            return _mapper.Map<NotificationDto>(entity);
        }
    }
}
