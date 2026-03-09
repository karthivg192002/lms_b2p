using iucs.lms.application.DTOs.Batch;
using iucs.lms.application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iucs.lms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchesController : ControllerBase
    {
        private readonly IBatchService _service;

        public BatchesController(IBatchService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBatchDto dto)
        {
            try
            {
                return Ok(await _service.CreateAsync(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _service.GetAllAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("add-student")]
        public async Task<IActionResult> AddStudent(AddBatchStudentDto dto)
        {
            try
            {
                await _service.AddStudent(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("remove-student")]
        public async Task<IActionResult> RemoveStudent(AddBatchStudentDto dto)
        {
            try
            {
                await _service.RemoveStudent(dto);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
