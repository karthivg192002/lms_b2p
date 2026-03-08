using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class QuizBase : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int TotalMarks { get; set; }
    public int PassingMarks { get; set; }
    public int DurationMinutes { get; set; }
    public int? TopicId { get; set; }
}

public class Quiz : QuizBase
{
    public Topic? Topic { get; set; }
    public ICollection<QuizQuestion> Questions { get; set; } = new List<QuizQuestion>();
    public ICollection<QuizAttempt> Attempts { get; set; } = new List<QuizAttempt>();
}
