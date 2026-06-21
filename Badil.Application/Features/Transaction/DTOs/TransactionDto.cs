namespace Badil.Application.Features.Transaction.DTOs
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public Guid ListingId { get; set; }
        public Guid BuyerId { get; set; }
        public Guid SellerId { get; set; }
        public decimal AgreedPrice { get; set; }
        public string EscrowState { get; set; }
        public bool IsSampleRequest { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
