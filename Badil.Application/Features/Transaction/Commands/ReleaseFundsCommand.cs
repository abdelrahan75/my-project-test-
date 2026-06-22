using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Domain.Interfaces;
using MediatR;

namespace Badil.Application.Features.Transaction.Commands
{
    public class ReleaseFundsCommand : IRequest
    {
        public Guid TransactionId { get; set; }
    }

    public class ReleaseFundsCommandHandler : IRequestHandler<ReleaseFundsCommand>
    {
        private readonly ITransactionRepository _repository;
        private readonly IEscrowGateway _escrowGateway;
        private readonly INotificationService _notificationService;

        public ReleaseFundsCommandHandler(ITransactionRepository repository, IEscrowGateway escrowGateway, INotificationService notificationService)
        {
            _repository = repository;
            _escrowGateway = escrowGateway;
            _notificationService = notificationService;
        }

        public async Task Handle(ReleaseFundsCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _repository.GetByIdAsync(request.TransactionId, cancellationToken);
            if (transaction == null)
                throw new KeyNotFoundException("Transaction not found");

            await _escrowGateway.ReleaseFundsAsync(transaction, cancellationToken);
            await _repository.UpdateAsync(transaction, cancellationToken);
            await _notificationService.SendTransactionUpdateAsync(transaction.SellerId, transaction.Id, "Funds released");
        }
    }
}
