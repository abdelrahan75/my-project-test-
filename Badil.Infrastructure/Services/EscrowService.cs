using Badil.Domain.Entity;
using Badil.Domain.Enum;
using Badil.Domain.Interfaces;

namespace Badil.Infrastructure.Services
{
    public class EscrowService : IEscrowGateway
    {
        public Task<bool> LockFundsAsync(Transaction transaction, CancellationToken ct = default)
        {
            if (transaction.EscrowState == EscrowStatus.AwaitingDeposit)
            {
                transaction.EscrowState = EscrowStatus.FundsLocked;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> ConfirmDeliveryAsync(Transaction transaction, CancellationToken ct = default)
        {
            if (transaction.EscrowState == EscrowStatus.FundsLocked)
            {
                transaction.EscrowState = EscrowStatus.InTransit;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> ReleaseFundsAsync(Transaction transaction, CancellationToken ct = default)
        {
            if (transaction.EscrowState == EscrowStatus.InspectionPeriod || transaction.EscrowState == EscrowStatus.InTransit)
            {
                transaction.EscrowState = EscrowStatus.FundsReleased;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> RefundAsync(Transaction transaction, CancellationToken ct = default)
        {
            if (transaction.EscrowState == EscrowStatus.FundsLocked || transaction.EscrowState == EscrowStatus.InTransit)
            {
                transaction.EscrowState = EscrowStatus.AwaitingDeposit;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<EscrowStatus> GetStatusAsync(Transaction transaction, CancellationToken ct = default)
        {
            return Task.FromResult(transaction.EscrowState);
        }
    }
}
