using AutoMapper;
using iucs.lms.domain.Repositories;
using iucs.lms.domain.Entities;
using iucs.lms.api.DTOs.Users;

namespace iucs.lms.api.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    private readonly IMapper _mapper;

    public UserService(IRepository<User> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user == null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        var user = _mapper.Map<User>(createUserDto);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);
        
        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
        
        return _mapper.Map<UserDto>(user);
    }

    public async Task UpdateUserAsync(int id, UpdateUserDto updateUserDto)
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

    public async Task DeleteUserAsync(int id)
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
