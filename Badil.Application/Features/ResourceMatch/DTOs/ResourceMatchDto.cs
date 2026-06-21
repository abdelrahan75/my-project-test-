namespace Badil.Application.Features.ResourceMatch.DTOs
{
    public class ResourceMatchDto
    {
        public Guid Id { get; set; }
        public Guid ListingId { get; set; }
        public Guid RequestId { get; set; }
        public double SemanticCompatibilityScore { get; set; }
        public double DistanceKm { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
