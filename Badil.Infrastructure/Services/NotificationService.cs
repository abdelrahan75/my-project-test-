using Badil.Application.Common.Interfaces;
using Badil.Domain.Entity;
using Badil.Domain.Enum;

namespace Badil.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IAppDbContext _context;

        public NotificationService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task SendNotificationAsync(Guid userId, string content, NotificationType type)
        {
            var notification = new Notification
            {
                UserId = userId,
                Content = content,
                Type = type,
                IsRead = false
            };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task SendMatchNotificationAsync(Guid userId, Guid matchId, string matchedMaterial)
        {
            await SendNotificationAsync(userId,
                $"New match found: {matchedMaterial}",
                NotificationType.MatchFound);
        }

        public async Task SendTransactionUpdateAsync(Guid userId, Guid transactionId, string status)
        {
            await SendNotificationAsync(userId,
                $"Transaction #{transactionId}: {status}",
                NotificationType.TransactionUpdate);
        }

        public async Task<int> GetUnreadCountAsync(Guid userId)
        {
            return await Task.FromResult(0);
        }
    }
}
