using System.ComponentModel.DataAnnotations;

namespace iucs.lms.api.DTOs.Users;

public class CreateUserDto
{
    [Required]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
    
    [Required]
    public string Phone { get; set; } = string.Empty;
    
    [Required]
    public string UserType { get; set; } = string.Empty;
}
