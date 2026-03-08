using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class TopicBase : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid SubjectId { get; set; }
}

public class Topic : TopicBase
{
    public Subject Subject { get; set; } = null!;
}
