using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using iucs.lms.application.DTOs.Role;
using iucs.lms.domain.Entities;
using iucs.lms.domain.Repositories;

namespace iucs.lms.application.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetRoles();
        Task<RoleDto?> GetRole(Guid id);
        Task<RoleDto> CreateRole(CreateRoleDto dto);
        Task UpdateRole(Guid id, CreateRoleDto dto);
        Task DeleteRole(Guid id);
    }
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<RoleMenu> _roleMenuRepository;
        private readonly IMapper _mapper;

        public RoleService(IRepository<Role> roleRepository,
        IRepository<RoleMenu> roleMenuRepository,
        IMapper mapper)
        {
            _roleRepository = roleRepository;
            _roleMenuRepository = roleMenuRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<RoleDto?> GetRole(Guid id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> CreateRole(CreateRoleDto dto)
        {
            var role = _mapper.Map<Role>(dto);

            role.Id = Guid.NewGuid();
            role.CreatedAt = DateTime.UtcNow;

            await _roleRepository.AddAsync(role);

            // Create RoleMenu Permissions
            if (dto.Permissions != null && dto.Permissions.Any())
            {
                foreach (var permission in dto.Permissions)
                {
                    var roleMenu = new RoleMenu
                    {
                        RoleId = role.Id,
                        MenuId = permission.MenuId,
                        CanCreate = permission.CanCreate,
                        CanRead = permission.CanRead,
                        CanUpdate = permission.CanUpdate,
                        CanDelete = permission.CanDelete
                    };

                    await _roleMenuRepository.AddAsync(roleMenu);
                }
            }

            await _roleRepository.SaveChangesAsync();

            return _mapper.Map<RoleDto>(role);
        }

        public async Task UpdateRole(Guid id, CreateRoleDto dto)
        {
            var role = await _roleRepository.GetByIdAsync(id);

            if (role == null)
                throw new Exception("Role not found");

            role.Name = dto.Name;
            role.Description = dto.Description;

            _roleRepository.Update(role);

            await _roleRepository.SaveChangesAsync();
        }

        public async Task DeleteRole(Guid id)
        {
            var role = await _roleRepository.GetByIdAsync(id);

            if (role == null)
                throw new Exception("Role not found");

            _roleRepository.Remove(role);

            await _roleRepository.SaveChangesAsync();
        }
    }
}
