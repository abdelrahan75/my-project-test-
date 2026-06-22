namespace Badil.Application.Common.Interfaces
{
    public interface IPdfReportService
    {
        Task<byte[]> GenerateEnvironmentalImpactReportAsync(Guid companyId, DateTime from, DateTime to);
        Task<byte[]> GenerateComplianceReportAsync(Guid companyId);
    }
}
