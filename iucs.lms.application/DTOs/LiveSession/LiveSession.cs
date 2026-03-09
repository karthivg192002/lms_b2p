using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iucs.lms.application.DTOs.LiveSession
{
    public record LiveSessionDto(Guid Id, Guid BatchId, Guid TeacherId, DateTime StartTime, 
        DateTime EndTime, string MeetingUrl, string Status);
    public record CreateLiveSessionDto(Guid BatchId, Guid TeacherId, DateTime StartTime,DateTime EndTime, 
        string MeetingUrl);
}
