namespace iucs.lms.domain.Entities;

public class LiveSession
{
    public int Id { get; set; }
    
    public int BatchId { get; set; }
    public Batch Batch { get; set; } = null!;
    
    public int TeacherId { get; set; }
    public User Teacher { get; set; } = null!;
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string MeetingUrl { get; set; } = string.Empty;
    public string Status { get; set; } = "Scheduled"; // Scheduled, Ongoing, Completed, Cancelled
}
