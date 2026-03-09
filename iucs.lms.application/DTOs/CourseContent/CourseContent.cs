using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iucs.lms.application.DTOs.CourseContent
{
    public record CourseContentDto(Guid Id, Guid CourseId, string Title, string ContentType, string ContentUrl);
    public record CreateCourseContentDto(Guid CourseId, string Title, string ContentType, string ContentUrl);
}
