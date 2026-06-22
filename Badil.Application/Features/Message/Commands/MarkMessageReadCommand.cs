using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using MediatR;

namespace Badil.Application.Features.Message.Commands
{
    public class MarkMessageReadCommand : IRequest
    {
        public Guid MessageId { get; set; }
    }

    public class MarkMessageReadCommandHandler : IRequestHandler<MarkMessageReadCommand>
    {
        private readonly IMessageRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public MarkMessageReadCommandHandler(IMessageRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task Handle(MarkMessageReadCommand request, CancellationToken cancellationToken)
        {
            var message = await _repository.GetByIdAsync(request.MessageId, cancellationToken);
            if (message == null)
                throw new KeyNotFoundException("Message not found");
            if (message.ReceiverId != _currentUserService.UserId)
                throw new UnauthorizedAccessException("You cannot mark this message as read");
            message.IsRead = true;
            await _repository.UpdateAsync(message, cancellationToken);
        }
    }
}
