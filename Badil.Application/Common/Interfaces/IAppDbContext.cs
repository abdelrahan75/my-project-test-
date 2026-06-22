using Badil.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Badil.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        //DbSet<AppUser> AppUsers { get; }
        DbSet<AppUser> Users { get; }
        DbSet<Company> Companies { get; }
        DbSet<DisputeTicket> DisputeTickets { get; }
        DbSet<MaterialRequest> MaterialRequests { get; }
        DbSet<Message> Messages { get; }
        DbSet<Notification> Notifications { get; }
        DbSet<ResourceMatch> ResourceMatches { get; }
        DbSet<Transaction> Transactions { get; }
        DbSet<VerificationRequest> VerificationRequests { get; }
        DbSet<WasteListing> WasteListings { get; }

        // This is the "Command" to save everything we've changed.
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
