using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iucs.lms.application.DTOs.Topic
{
    public record TopicDto(Guid Id, string Name, string? Description, Guid SubjectId);
    public record CreateTopicDto(string Name, string? Description, Guid SubjectId);
    public record UpdateTopicDto(string Name, string? Description, Guid SubjectId);
}
