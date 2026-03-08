using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class BatchTeacherBase : BaseEntity
{
    public int BatchId { get; set; }
    public int TeacherId { get; set; }
}

public class BatchTeacher : BatchTeacherBase
{
    public Batch Batch { get; set; } = null!;
    public User Teacher { get; set; } = null!;
}
