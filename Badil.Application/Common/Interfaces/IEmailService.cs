namespace Badil.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
        Task SendEmailWithTemplateAsync(string to, string templateName, Dictionary<string, string> placeholders);
    }
}
