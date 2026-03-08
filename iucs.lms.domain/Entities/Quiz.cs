namespace iucs.lms.domain.Entities;

public class Quiz
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int TotalMarks { get; set; }
    public int PassingMarks { get; set; }
    
    // Duration in minutes
    public int DurationMinutes { get; set; }
    
    // Foreign Keys mapping conceptually to a Topic or Course
    public int? TopicId { get; set; }
    public Topic? Topic { get; set; }
    
    public ICollection<QuizQuestion> Questions { get; set; } = new List<QuizQuestion>();
    public ICollection<QuizAttempt> Attempts { get; set; } = new List<QuizAttempt>();
}
