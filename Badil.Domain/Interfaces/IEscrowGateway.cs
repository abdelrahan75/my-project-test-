using Badil.Domain.Entity;
using Badil.Domain.Enum;

namespace Badil.Domain.Interfaces
{
    public interface IEscrowGateway
    {
        Task<bool> LockFundsAsync(Transaction transaction, CancellationToken ct = default);
        Task<bool> ConfirmDeliveryAsync(Transaction transaction, CancellationToken ct = default);
        Task<bool> ReleaseFundsAsync(Transaction transaction, CancellationToken ct = default);
        Task<bool> RefundAsync(Transaction transaction, CancellationToken ct = default);
        Task<EscrowStatus> GetStatusAsync(Transaction transaction, CancellationToken ct = default);
    }
}
