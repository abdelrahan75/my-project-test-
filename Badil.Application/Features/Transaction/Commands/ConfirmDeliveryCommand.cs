using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Domain.Interfaces;
using MediatR;

namespace Badil.Application.Features.Transaction.Commands
{
    public class ConfirmDeliveryCommand : IRequest
    {
        public Guid TransactionId { get; set; }
    }

    public class ConfirmDeliveryCommandHandler : IRequestHandler<ConfirmDeliveryCommand>
    {
        private readonly ITransactionRepository _repository;
        private readonly IEscrowGateway _escrowGateway;
        private readonly INotificationService _notificationService;

        public ConfirmDeliveryCommandHandler(ITransactionRepository repository, IEscrowGateway escrowGateway, INotificationService notificationService)
        {
            _repository = repository;
            _escrowGateway = escrowGateway;
            _notificationService = notificationService;
        }

        public async Task Handle(ConfirmDeliveryCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _repository.GetByIdAsync(request.TransactionId, cancellationToken);
            if (transaction == null)
                throw new KeyNotFoundException("Transaction not found");

            await _escrowGateway.ConfirmDeliveryAsync(transaction, cancellationToken);
            await _repository.UpdateAsync(transaction, cancellationToken);
            await _notificationService.SendTransactionUpdateAsync(transaction.BuyerId, transaction.Id, "Delivery confirmed - inspection period started");
        }
    }
}
