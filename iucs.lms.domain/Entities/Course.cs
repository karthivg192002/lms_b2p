namespace iucs.lms.domain.Entities;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsPublished { get; set; } = false;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public ICollection<CourseContent> CourseContents { get; set; } = new List<CourseContent>();
    public ICollection<Batch> Batches { get; set; } = new List<Batch>();
}
