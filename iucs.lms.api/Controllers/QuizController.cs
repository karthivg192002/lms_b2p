using iucs.lms.application.DTOs.Quiz;
using iucs.lms.application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iucs.lms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _service;

        public QuizController(IQuizService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateQuizDto dto)
        {
            try
            {
                return Ok(await _service.CreateQuiz(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("questions")]
        public async Task<IActionResult> AddQuestion(CreateQuizQuestionDto dto)
        {
            try
            {
                await _service.AddQuestion(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("attempt")]
        public async Task<IActionResult> Attempt(QuizAttemptDto dto)
        {
            try
            {
                await _service.SubmitAttempt(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("result")]
        public async Task<IActionResult> Result(Guid quizId, Guid studentId)
        {
            try
            {
                return Ok(await _service.GetResult(quizId, studentId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
