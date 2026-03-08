namespace iucs.lms.domain.Entities;

public class Topic
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    // Foreign Key
    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;
}
