using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iucs.lms.application.DTOs.Batch
{
    public record BatchDto(Guid Id, string Name, Guid CourseId, Guid TeacherId, DateTime StartDate, DateTime EndDate);
    public record CreateBatchDto(string Name, Guid CourseId, Guid TeacherId, DateTime StartDate,DateTime EndDate);
    public record AddBatchStudentDto(Guid BatchId, Guid StudentId);
}
