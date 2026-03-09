using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iucs.lms.application.DTOs.Quiz
{
    public record CreateQuizDto(string Title, string Description, int TotalMarks, int PassingMarks,
        int DurationMinutes, Guid? TopicId);
    public record QuizDto(Guid Id, string Title, int TotalMarks, int PassingMarks, int DurationMinutes);
    public record CreateQuizQuestionDto(Guid QuizId, string QuestionText, string OptionA, string OptionB,
        string OptionC, string OptionD, string CorrectOption, int Marks);
    public record QuizAttemptDto(Guid QuizId, Guid StudentId, int Score, bool Passed);
}
