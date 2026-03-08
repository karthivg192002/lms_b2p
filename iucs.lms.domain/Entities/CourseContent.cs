using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class CourseContentBase : BaseEntity
{
    public string ContentType { get; set; } = string.Empty; // Video or PDF
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public Guid CourseId { get; set; }
    public Guid TopicId { get; set; }
}

public class CourseContent : CourseContentBase
{
    public Course Course { get; set; } = null!;
    public Topic Topic { get; set; } = null!;
}
