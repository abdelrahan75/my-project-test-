using Badil.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Badil.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            await Task.CompletedTask;
        }

        public async Task SendEmailWithTemplateAsync(string to, string templateName, Dictionary<string, string> placeholders)
        {
            await Task.CompletedTask;
        }
    }
}
