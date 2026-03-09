using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iucs.lms.application.DTOs.Subject
{
    public record SubjectDto(Guid Id, string Name, string? Description, Guid ClassId);
    public record CreateSubjectDto(string Name, string? Description, Guid ClassId);
    public record UpdateSubjectDto(string Name, string? Description, Guid ClassId);
}
