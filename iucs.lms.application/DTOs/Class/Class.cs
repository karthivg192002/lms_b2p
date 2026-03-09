using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iucs.lms.application.DTOs.Class
{
    public record ClassDto(Guid Id, string Name, string? Description, Guid BoardId);
    public record CreateClassDto(string Name, string? Description, Guid BoardId);
    public record UpdateClassDto(string Name, string? Description, Guid BoardId);
}
