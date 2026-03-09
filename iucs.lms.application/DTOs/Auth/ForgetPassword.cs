using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iucs.lms.application.DTOs.Auth
{
    public class ForgotPasswordDto
    {
        public string Email { get; set; } = "";
    }
    public class ResetPasswordDto
    {
        public string ResetToken { get; set; } = "";
        public string NewPassword { get; set; } = "";
    }
}
