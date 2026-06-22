using Badil.Application.Common.Interfaces;

namespace Badil.Infrastructure.Services
{
    public class PdfReportService : IPdfReportService
    {
        public async Task<byte[]> GenerateEnvironmentalImpactReportAsync(Guid companyId, DateTime from, DateTime to)
        {
            var report = System.Text.Encoding.UTF8.GetBytes(
                $"Environmental Impact Report\nCompany: {companyId}\nPeriod: {from:yyyy-MM-dd} to {to:yyyy-MM-dd}\nCO2 Saved: 0 tons\nWaste Diverted: 0 tons");
            await Task.CompletedTask;
            return report;
        }

        public async Task<byte[]> GenerateComplianceReportAsync(Guid companyId)
        {
            var report = System.Text.Encoding.UTF8.GetBytes(
                $"Compliance Report\nCompany: {companyId}\nStatus: Generated");
            await Task.CompletedTask;
            return report;
        }
    }
}
