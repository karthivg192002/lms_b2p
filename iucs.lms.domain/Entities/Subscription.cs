using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class SubscriptionBase : AuditEntity
{
    public Guid UserId { get; set; }    
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime EndDate { get; set; }
    public string PlanName { get; set; } = string.Empty; // Monthly, Yearly
    public decimal Price { get; set; }
    public bool AutoRenew { get; set; } = true;
}

public class Subscription : SubscriptionBase
{
    public User User { get; set; } = null!;
}
