using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class BatchStudentBase : BaseEntity
{
    public Guid BatchId { get; set; }
    public Guid StudentId { get; set; }
}

public class BatchStudent : BatchStudentBase
{
    public Batch Batch { get; set; } = null!;
    public User Student { get; set; } = null!;
}
