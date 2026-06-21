using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using MediatR;

namespace Badil.Application.Features.Message.Commands
{
    public class DeleteMessageCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand>
    {
        private readonly IMessageRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteMessageCommandHandler(IMessageRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Message not found");

            if (entity.SenderId != _currentUserService.UserId && !_currentUserService.IsAdmin)
                throw new UnauthorizedAccessException("You are not allowed to delete this message.");

            await _repository.DeleteAsync(entity, cancellationToken);
        }
    }
}
