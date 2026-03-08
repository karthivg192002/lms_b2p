using iucs.lms.application.DTOs.Role;
using iucs.lms.application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iucs.lms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _service.GetRoles());
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleDto dto)
        {
            var result = await _service.CreateRole(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(Guid id, CreateRoleDto dto)
        {
            await _service.UpdateRole(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            await _service.DeleteRole(id);
            return Ok();
        }
    }
}
