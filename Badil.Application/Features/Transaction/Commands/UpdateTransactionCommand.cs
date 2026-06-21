using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Domain.Enum;
using MediatR;

namespace Badil.Application.Features.Transaction.Commands
{
    public class UpdateTransactionCommand : IRequest
    {
        public Guid Id { get; set; }
        public decimal AgreedPrice { get; set; }
        public EscrowStatus EscrowState { get; set; }
        public bool IsSampleRequest { get; set; }
    }

    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand>
    {
        private readonly ITransactionRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateTransactionCommandHandler(ITransactionRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Transaction not found");

            var currentUserId = _currentUserService.UserId;
            if (entity.BuyerId != currentUserId && entity.SellerId != currentUserId && !_currentUserService.IsAdmin)
                throw new UnauthorizedAccessException("You are not allowed to modify this transaction.");

            entity.AgreedPrice = request.AgreedPrice;
            entity.EscrowState = request.EscrowState;
            entity.IsSampleRequest = request.IsSampleRequest;

            await _repository.UpdateAsync(entity, cancellationToken);
        }
    }
}
