namespace Badil.Application.Features.WasteListing.DTOs
{
    public class WasteListingDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string MaterialType { get; set; }
        public string AIStandardizedTag { get; set; }
        public double Quantity { get; set; }
        public string Description { get; set; }
        public List<string> ImageUrls { get; set; }
        public decimal SuggestedPrice { get; set; }
        public string Status { get; set; }
        public bool IsVisuallyValidated { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
