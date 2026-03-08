using System.Security.Claims;
using iucs.lms.application.DTOs.Menu;
using iucs.lms.application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iucs.lms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _service;

        public MenuController(IMenuService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetMenus()
        {
            return Ok(await _service.GetMenus());
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenu(CreateMenuDto dto)
        {
            try
            {
                return Ok(await _service.CreateMenu(dto));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenu(Guid id, CreateMenuDto dto)
        {
            await _service.UpdateMenu(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(Guid id)
        {
            await _service.DeleteMenu(id);
            return Ok();
        }

        [HttpGet("get-user-access-menu")]
        public async Task<IActionResult> GetSidebar()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var menus = await _service.GetUserMenus(userId);

            return Ok(menus);
        }
    }
}
