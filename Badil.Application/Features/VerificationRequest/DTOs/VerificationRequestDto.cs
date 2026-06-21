namespace Badil.Application.Features.VerificationRequest.DTOs
{
    public class VerificationRequestDto
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public List<string> DocumentUrls { get; set; }
        public string Status { get; set; }
        public Guid? ReviewedByAdminId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
