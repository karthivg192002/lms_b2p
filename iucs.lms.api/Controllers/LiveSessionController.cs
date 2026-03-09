using iucs.lms.application.DTOs.LiveSession;
using iucs.lms.application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iucs.lms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiveSessionController : ControllerBase
    {
        private readonly ILiveSessionService _service;

        public LiveSessionController(ILiveSessionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLiveSessionDto dto)
        {
            try
            {
                return Ok(await _service.CreateAsync(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("batch/{batchId}")]
        public async Task<IActionResult> GetByBatch(Guid batchId)
        {
            try
            {
                return Ok(await _service.GetByBatch(batchId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, string status)
        {
            try
            {
                return Ok(await _service.UpdateStatus(id, status));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
