using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public enum LiveSessionStatus
{
    Scheduled = 1,
    Ongoing = 2,
    Completed = 3,
    Cancelled = 4
}

public class LiveSessionBase : BaseEntity
{
    public Guid BatchId { get; set; }
    public Guid TeacherId { get; set; }    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string MeetingUrl { get; set; } = string.Empty;
    public LiveSessionStatus Status { get; set; } = LiveSessionStatus.Scheduled;
}

public class LiveSession : LiveSessionBase
{
    public Batch Batch { get; set; } = null!;
    public User Teacher { get; set; } = null!;
}
