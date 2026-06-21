namespace Badil.Application.Features.Company.DTOs
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Sector { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsVerified { get; set; }
        public string TaxRegistrationNumber { get; set; }
        public string CommercialRegisterNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
