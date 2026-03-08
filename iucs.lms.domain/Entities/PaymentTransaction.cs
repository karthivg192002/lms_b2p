namespace iucs.lms.domain.Entities;

public class PaymentTransaction
{
    public int Id { get; set; }
    
    // Foreign Keys
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    // Optional CourseId or SubscriptionId depending on what was purchased
    public int? CourseId { get; set; }
    public Course? Course { get; set; }
    
    public int? SubscriptionId { get; set; }
    public Subscription? Subscription { get; set; }
    
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public string PaymentProvider { get; set; } = string.Empty; // Stripe, Razorpay
    public string TransactionId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty; // Success, Failed, Pending
    
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
}
