using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using MediatR;

namespace Badil.Application.Features.Message.Commands
{
    public class UpdateMessageCommand : IRequest
    {
        public Guid Id { get; set; }
        public bool IsRead { get; set; }
    }

    public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand>
    {
        private readonly IMessageRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateMessageCommandHandler(IMessageRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Message not found");

            if (entity.ReceiverId != _currentUserService.UserId && entity.SenderId != _currentUserService.UserId && !_currentUserService.IsAdmin)
                throw new UnauthorizedAccessException("You are not allowed to modify this message.");

            entity.IsRead = request.IsRead;

            await _repository.UpdateAsync(entity, cancellationToken);
        }
    }
}
