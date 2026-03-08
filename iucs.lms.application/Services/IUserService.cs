using iucs.lms.api.DTOs.Users;

namespace iucs.lms.api.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
    Task UpdateUserAsync(int id, UpdateUserDto updateUserDto);
    Task DeleteUserAsync(int id);
}
