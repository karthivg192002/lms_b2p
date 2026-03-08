namespace iucs.lms.domain.Entities;

public class QuizAttempt
{
    public int Id { get; set; }
    
    // Foreign Keys
    public int QuizId { get; set; }
    public Quiz Quiz { get; set; } = null!;
    
    public int StudentId { get; set; }
    public User Student { get; set; } = null!;
    
    public DateTime AttemptDate { get; set; } = DateTime.UtcNow;
    public int Score { get; set; }
    public bool Passed { get; set; }
}
