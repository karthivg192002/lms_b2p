using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class BatchStudentBase : BaseEntity
{
    public int BatchId { get; set; }
    public int StudentId { get; set; }
}

public class BatchStudent : BatchStudentBase
{
    public Batch Batch { get; set; } = null!;
    public User Student { get; set; } = null!;
}
