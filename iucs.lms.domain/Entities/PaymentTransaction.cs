using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class PaymentTransactionBase : AuditEntity
{
    public int UserId { get; set; }
    public int? CourseId { get; set; }
    public int? SubscriptionId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public string PaymentProvider { get; set; } = string.Empty; // Stripe, Razorpay
    public string TransactionId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty; // Success, Failed, Pending
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
}

public class PaymentTransaction : PaymentTransactionBase
{
    public User User { get; set; } = null!;
    public Course? Course { get; set; }
    public Subscription? Subscription { get; set; }
}