using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class QuizAttemptBase : BaseEntity
{
    public int QuizId { get; set; }
    public int StudentId { get; set; }
    public DateTime AttemptDate { get; set; } = DateTime.UtcNow;
    public int Score { get; set; }
    public bool Passed { get; set; }
}

public class QuizAttempt : QuizAttemptBase
{
    public Quiz Quiz { get; set; } = null!;
    public User Student { get; set; } = null!;
}