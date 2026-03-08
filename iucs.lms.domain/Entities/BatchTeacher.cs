namespace iucs.lms.domain.Entities;

public class BatchTeacher
{
    public int BatchId { get; set; }
    public Batch Batch { get; set; } = null!;
    
    public int TeacherId { get; set; }
    public User Teacher { get; set; } = null!;
}
