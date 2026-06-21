namespace Badil.Application.Features.MaterialRequest.DTOs
{
    public class MaterialRequestDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string MaterialType { get; set; }
        public double TargetQuantity { get; set; }
        public double LocationPreferenceRadiusKm { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
