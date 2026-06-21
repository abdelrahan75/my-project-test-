
using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Infrastructure.Repositories;
using Badil.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Badil.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
           services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
           services.AddScoped<IUserRepository, UserRepository>();
           services.AddScoped<ICompanyRepository, CompanyRepository>();
           services.AddScoped<IMaterialRequestRepository, MaterialRequestRepository>();
           services.AddScoped<IMessageRepository, MessageRepository>();
           services.AddScoped<INotificationRepository, NotificationRepository>();
           services.AddScoped<IResourceMatchRepository, ResourceMatchRepository>();
           services.AddScoped<ITransactionRepository, TransactionRepository>();
           services.AddScoped<IDisputeTicketRepository, DisputeTicketRepository>();
           services.AddScoped<IVerificationRequestRepository, VerificationRequestRepository>();
           services.AddScoped<IWasteListingRepository, WasteListingRepository>();

           services.AddScoped<IFileService, FileService>();
           services.AddScoped<ITokenService, TokenService>();

           services.AddHttpContextAccessor();
           services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
