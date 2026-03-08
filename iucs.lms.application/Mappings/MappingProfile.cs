using AutoMapper;
using iucs.lms.domain.Entities;
using iucs.lms.api.DTOs.Users;
using iucs.lms.api.DTOs.Auth;
using iucs.lms.application.DTOs.Menu;
using iucs.lms.application.DTOs.Role;
using iucs.lms.application.DTOs.Permission;

namespace iucs.lms.api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        
        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));
            
        CreateMap<UpdateUserDto, User>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            
        CreateMap<RegisterDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => "Student"))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<CreateRoleDto, Role>();

        CreateMap<Menu, MenuDto>().ReverseMap();
        CreateMap<CreateMenuDto, Menu>();

        CreateMap<RoleMenuPermissionDto, RoleMenu>().ReverseMap();
    }
}
