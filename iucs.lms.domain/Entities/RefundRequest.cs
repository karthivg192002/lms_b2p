namespace iucs.lms.domain.Entities;

public class RefundRequest
{
    public int Id { get; set; }
    
    // Foreign Key
    public int PaymentTransactionId { get; set; }
    public PaymentTransaction PaymentTransaction { get; set; } = null!;
    
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public string Reason { get; set; } = string.Empty;
    public decimal RefundAmount { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected, Processed
    
    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ProcessedAt { get; set; }
}
