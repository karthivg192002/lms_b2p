using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class ClassBase : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid BoardId { get; set; }
}

public class Class : ClassBase
{
    public Board Board { get; set; } = null!;
    public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
