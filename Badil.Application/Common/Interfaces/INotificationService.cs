using Badil.Domain.Enum;

namespace Badil.Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationAsync(Guid userId, string content, NotificationType type);
        Task SendMatchNotificationAsync(Guid userId, Guid matchId, string matchedMaterial);
        Task SendTransactionUpdateAsync(Guid userId, Guid transactionId, string status);
        Task<int> GetUnreadCountAsync(Guid userId);
    }
}
