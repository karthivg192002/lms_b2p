using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iucs.lms.domain.Entities
{
    public class AuthCode
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Code { get; set; } = "";
        public string Purpose { get; set; } = "";
        // Login / ResetPassword / EmailVerification
        public string ResetToken { get; set; } = "";
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
