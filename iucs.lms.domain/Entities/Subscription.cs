namespace iucs.lms.domain.Entities;

public class Subscription
{
    public int Id { get; set; }
    
    // Foreign Key
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime EndDate { get; set; }
    
    public string PlanName { get; set; } = string.Empty; // Monthly, Yearly
    public decimal Price { get; set; }
    public bool IsActive { get; set; } = true;
    public bool AutoRenew { get; set; } = true;
}
