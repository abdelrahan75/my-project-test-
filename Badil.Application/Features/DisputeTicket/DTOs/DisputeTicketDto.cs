namespace Badil.Application.Features.DisputeTicket.DTOs
{
    public class DisputeTicketDto
    {
        public Guid Id { get; set; }
        public Guid TransactionId { get; set; }
        public Guid RaisedByUserId { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string AdminResolutionRemarks { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
