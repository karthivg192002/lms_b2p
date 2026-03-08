using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class RefundRequestBase : AuditEntity
{
    public int PaymentTransactionId { get; set; }
    public int UserId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public decimal RefundAmount { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected, Processed
    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ProcessedAt { get; set; }
}

public class RefundRequest : RefundRequestBase
{
    public PaymentTransaction PaymentTransaction { get; set; } = null!;
    public User User { get; set; } = null!;
}
