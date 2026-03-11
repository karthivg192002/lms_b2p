using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.lms.application.DTOs.Communication;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace iucs.lms.application.Services
{
    public interface ICommunicationService
    {
        Task SendOtpAsync(string email, string username, string otp);
        Task SendResetMailEmailAsync(string email, string username, string resetLink);
    }
    public class CommunicationService : ICommunicationService
    {
        private readonly IConfiguration _config;
        public CommunicationService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendOtpAsync(string email, string username, string otp)
        {
            var baseUrl = _config["CommunicationUrl"];

            using var httpClient = new HttpClient();

            var templateResponse = await httpClient.GetAsync($"{baseUrl}Template/getbytemplate/ForgetPassword");
            templateResponse.EnsureSuccessStatusCode();

            var templateJson = await templateResponse.Content.ReadAsStringAsync();
            var template = JsonConvert.DeserializeObject<TemplateDto>(templateJson);

            string subject = template.Subject;
            string body = template.Body;
            body = body.Replace("{username}", username)
                       .Replace("{time}", "5")
                       .Replace("{otp}", otp)
                       .Replace("{company}", "Infinity Uniquers");

            var communication = new CommunicationRequest
            {
                To = email,
                Subject = subject,
                Message = body,
                Type = "smtp",
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(communication), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{baseUrl}sendcommunication/send", jsonContent);
            response.EnsureSuccessStatusCode();
        }

        public async Task SendResetMailEmailAsync(string email, string username, string resetLink)
        {
            var baseUrl = _config["CommunicationUrl"];

            using var httpClient = new HttpClient();

            var templateResponse = await httpClient.GetAsync($"{baseUrl}Template/getbytemplate/ResetPassword");
            templateResponse.EnsureSuccessStatusCode();

            var templateJson = await templateResponse.Content.ReadAsStringAsync();
            var template = JsonConvert.DeserializeObject<TemplateDto>(templateJson);

            string subject = template.Subject;
            string body = template.Body;
            body = body.Replace("{username}", username)
                       .Replace("{resetLink}", resetLink)
                       .Replace("{company}", "Infinity Uniquers");

            var communication = new CommunicationRequest
            {
                To = email,
                Subject = subject,
                Message = body,
                Type = "smtp",
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(communication), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{baseUrl}sendcommunication/send", jsonContent);
            response.EnsureSuccessStatusCode();
        }
    }
}
