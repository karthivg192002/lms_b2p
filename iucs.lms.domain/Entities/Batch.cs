using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class BatchBase : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid CourseId { get; set; }
}

public class Batch : BatchBase
{
    public Course Course { get; set; } = null!;
    public ICollection<BatchStudent> BatchStudents { get; set; } = new List<BatchStudent>();
    public ICollection<BatchTeacher> BatchTeachers { get; set; } = new List<BatchTeacher>();
    public ICollection<LiveSession> LiveSessions { get; set; } = new List<LiveSession>();
}
