namespace iucs.lms.domain.Entities;

public class Class
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    // Foreign Key
    public int BoardId { get; set; }
    public Board Board { get; set; } = null!;
    
    // Navigation properties
    public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
