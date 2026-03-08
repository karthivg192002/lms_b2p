namespace iucs.lms.domain.Entities;

public class CourseContent
{
    public int Id { get; set; }
    public string ContentType { get; set; } = string.Empty; // Video or PDF
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    
    // Foreign Keys
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
    
    public int TopicId { get; set; }
    public Topic Topic { get; set; } = null!;
}
