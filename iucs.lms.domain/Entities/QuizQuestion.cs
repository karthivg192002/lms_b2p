namespace iucs.lms.domain.Entities;

public class QuizQuestion
{
    public int Id { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public string OptionA { get; set; } = string.Empty;
    public string OptionB { get; set; } = string.Empty;
    public string OptionC { get; set; } = string.Empty;
    public string OptionD { get; set; } = string.Empty;
    
    // Contains A, B, C, or D
    public string CorrectOption { get; set; } = string.Empty;
    
    public int Marks { get; set; } = 1;

    // Foreign Key
    public int QuizId { get; set; }
    public Quiz Quiz { get; set; } = null!;
}
