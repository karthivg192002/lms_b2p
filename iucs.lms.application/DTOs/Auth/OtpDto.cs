using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iucs.lms.application.DTOs.Auth
{
    public class OtpDto
    {
        public string Email { get; set; } = "";
    }

    public class VerifyOtpDto
    {
        public string Email { get; set; } = "";
        public string Otp { get; set; } = "";
    }
}
