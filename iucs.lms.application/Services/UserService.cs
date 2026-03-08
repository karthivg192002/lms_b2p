using AutoMapper;
using iucs.lms.domain.Repositories;
using iucs.lms.domain.Entities;
using iucs.lms.api.DTOs.Users;

namespace iucs.lms.api.Services;
public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(Guid id);
    Task CreateUserAsync(CreateUserDto createUserDto);
    Task UpdateUserAsync(Guid id, UpdateUserDto updateUserDto);
    Task DeleteUserAsync(Guid id);
}
public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<UserRole> _userRoleRepository;
    private readonly IMapper _mapper;

    public UserService(IRepository<User> userRepository, IMapper mapper, IRepository<UserRole> userRoleRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user == null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task CreateUserAsync(CreateUserDto dto)
    {
        var user = _mapper.Map<User>(dto);

        user.Id = Guid.NewGuid();
        user.CreatedAt = DateTime.UtcNow;

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        await _userRepository.AddAsync(user);

        var userRole = new UserRole
        {
            UserId = user.Id,
            RoleId = dto.RoleId
        };

        await _userRoleRepository.AddAsync(userRole);

        await _userRepository.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(Guid id, UpdateUserDto updateUserDto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }
        
        _mapper.Map(updateUserDto, user);
        
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }
        
        _userRepository.Remove(user);
        await _userRepository.SaveChangesAsync();
    }
}
