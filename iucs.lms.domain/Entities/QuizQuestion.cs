using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class QuizQuestionBase : BaseEntity
{
    public string QuestionText { get; set; } = string.Empty;
    public string OptionA { get; set; } = string.Empty;
    public string OptionB { get; set; } = string.Empty;
    public string OptionC { get; set; } = string.Empty;
    public string OptionD { get; set; } = string.Empty;
    public string CorrectOption { get; set; } = string.Empty;
    public int Marks { get; set; } = 1;
    public Guid QuizId { get; set; }
}

public class QuizQuestion : QuizQuestionBase
{
    public Quiz Quiz { get; set; } = null!;
}
