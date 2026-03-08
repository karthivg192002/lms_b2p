namespace iucs.lms.domain.Entities;

public class Batch
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    // Foreign Key
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
    
    // Navigation properties
    public ICollection<BatchStudent> BatchStudents { get; set; } = new List<BatchStudent>();
    public ICollection<BatchTeacher> BatchTeachers { get; set; } = new List<BatchTeacher>();
    public ICollection<LiveSession> LiveSessions { get; set; } = new List<LiveSession>();
}
