using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using iucs.lms.application.DTOs.Menu;
using iucs.lms.domain.Entities;
using iucs.lms.domain.Repositories;

namespace iucs.lms.application.Services
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuDto>> GetMenus();
        Task<MenuDto?> GetMenu(Guid id);
        Task<MenuDto> CreateMenu(CreateMenuDto dto);
        Task UpdateMenu(Guid id, CreateMenuDto dto);
        Task DeleteMenu(Guid id);
        Task<IEnumerable<MenuDto>> GetUserMenus(Guid userId);
    }
    public class MenuService : IMenuService
    {
        private readonly IRepository<Menu> _repository;
        private readonly IRepository<UserRole> _userRoleRepo;
        private readonly IRepository<RoleMenu> _roleMenuRepo;
        private readonly IMapper _mapper;

        public MenuService(IRepository<Menu> repository, IMapper mapper,
            IRepository<UserRole> userRoleRepo, IRepository<RoleMenu> roleMenuRepo)
        {
            _repository = repository;
            _mapper = mapper;
            _userRoleRepo = userRoleRepo;
            _roleMenuRepo = roleMenuRepo;
        }

        public async Task<IEnumerable<MenuDto>> GetMenus()
        {
            var menus = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<MenuDto>>(menus);
        }

        public async Task<MenuDto?> GetMenu(Guid id)
        {
            var menu = await _repository.GetByIdAsync(id);
            return _mapper.Map<MenuDto>(menu);
        }

        public async Task<MenuDto> CreateMenu(CreateMenuDto dto)
        {
            var menu = _mapper.Map<Menu>(dto);

            menu.CreatedAt = DateTime.UtcNow;

            await _repository.AddAsync(menu);
            await _repository.SaveChangesAsync();

            return _mapper.Map<MenuDto>(menu);
        }

        public async Task UpdateMenu(Guid id, CreateMenuDto dto)
        {
            var menu = await _repository.GetByIdAsync(id);

            if (menu == null)
                throw new Exception("Menu not found");

            _mapper.Map(dto, menu);

            _repository.Update(menu);

            await _repository.SaveChangesAsync();
        }

        public async Task DeleteMenu(Guid id)
        {
            var menu = await _repository.GetByIdAsync(id);

            _repository.Remove(menu);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<MenuDto>> GetUserMenus(Guid userId)
        {
            var userRole = (await _userRoleRepo.FindAsync(x => x.UserId == userId)).First();

            var roleMenus = await _roleMenuRepo.FindAsync(x => x.RoleId == userRole.RoleId);

            var menuIds = roleMenus.Select(x => x.MenuId);

            var menus = await _repository.FindAsync(x => menuIds.Contains(x.Id));

            return _mapper.Map<IEnumerable<MenuDto>>(menus);
        }
    }
}
