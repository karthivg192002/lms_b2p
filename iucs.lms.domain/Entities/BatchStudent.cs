namespace iucs.lms.domain.Entities;

public class BatchStudent
{
    public int BatchId { get; set; }
    public Batch Batch { get; set; } = null!;
    
    public int StudentId { get; set; }
    public User Student { get; set; } = null!;
}
