
using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Domain.Interfaces;
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
           services.AddScoped<IEmailService, EmailService>();
           services.AddScoped<IPdfReportService, PdfReportService>();
           services.AddScoped<INotificationService, NotificationService>();
           services.AddScoped<IUnitOfWork, UnitOfWork>();

           services.AddScoped<ISemanticMatchmaker, SemanticMatchmakerService>();
           services.AddScoped<IDynamicPricingAgent, DynamicPricingService>();
           services.AddScoped<IGeospatialAnalyzer, GeospatialAnalysisService>();
           services.AddScoped<IVisualInspectionAgent, VisualInspectionService>();
           services.AddScoped<IEscrowGateway, EscrowService>();

           services.AddHttpContextAccessor();
           services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
