namespace iucs.lms.application.DTOs.Course
{
    public record CourseDto(Guid Id, string Title, string Description, decimal Price, bool IsPublished);
    public record CreateCourseDto(string Title, string Description, decimal Price);
    public record UpdateCourseDto(string Title, string Description, decimal Price, bool IsPublished);
}
