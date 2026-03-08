using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class SubjectBase : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ClassId { get; set; }
}

public class Subject : SubjectBase
{
    public Class Class { get; set; } = null!;
    public ICollection<Topic> Topics { get; set; } = new List<Topic>();
}
