using Badil.Application.Common.Interfaces;
using Badil.Domain.Entity;
using Badil.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Badil.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<AppUser , IdentityRole<Guid> , Guid> , IAppDbContext
    {
        //public DbSet<AppUser> AppUsers { get; set; }
        // public DbSet<AppUser> Users { get; set; } It's already provided by IdentityDbContext
        public DbSet<Company> Companies { get; set; }
        public DbSet<DisputeTicket> DisputeTickets { get; set; }
        public DbSet<MaterialRequest> MaterialRequests { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ResourceMatch> ResourceMatches { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<VerificationRequest> VerificationRequests { get; set; }
        public DbSet<WasteListing> WasteListings { get; set; }


        private readonly ICurrentUserService _currentUserService;

        public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        if (_currentUserService.UserId.HasValue)
                            entry.Entity.CreatedBy = _currentUserService.UserId.Value;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        if (_currentUserService.UserId.HasValue)
                            entry.Entity.UpdatedBy = _currentUserService.UserId.Value;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
