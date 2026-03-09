using Newtonsoft.Json;

namespace iucs.lms.application.DTOs.Communication
{
    public class CommunicationRequest
    {
        public string To { get; set; } = default!;
        public string Subject { get; set; } = default!;
        public string Message { get; set; } = default!;
        public string Type { get; set; } = "smtp";
        public Guid ClientId { get; set; }
    }
    public class TemplateDto
    {
        [JsonProperty("subject")]
        public string Subject { get; set; } = default!;

        [JsonProperty("body")]
        public string Body { get; set; } = default!;
    }
}
