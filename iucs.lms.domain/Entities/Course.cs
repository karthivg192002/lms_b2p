using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class CourseBase : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsPublished { get; set; } = false;
}

public class Course : CourseBase
{
    public ICollection<CourseContent> CourseContents { get; set; } = new List<CourseContent>();
    public ICollection<Batch> Batches { get; set; } = new List<Batch>();
}
