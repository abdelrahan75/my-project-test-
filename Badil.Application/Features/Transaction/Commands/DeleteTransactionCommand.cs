using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using MediatR;

namespace Badil.Application.Features.Transaction.Commands
{
    public class DeleteTransactionCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand>
    {
        private readonly ITransactionRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteTransactionCommandHandler(ITransactionRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Transaction not found");

            if (!_currentUserService.IsAdmin)
                throw new UnauthorizedAccessException("Only an admin can delete a transaction.");

            await _repository.DeleteAsync(entity, cancellationToken);
        }
    }
}
