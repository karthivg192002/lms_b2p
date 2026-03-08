namespace iucs.lms.domain.Entities;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    // Foreign Key
    public int ClassId { get; set; }
    public Class Class { get; set; } = null!;
    
    // Navigation properties
    public ICollection<Topic> Topics { get; set; } = new List<Topic>();
}
