using AutoMapper;
using iucs.lms.domain.Entities;
using iucs.lms.api.DTOs.Users;
using iucs.lms.api.DTOs.Auth;
using iucs.lms.application.DTOs.Menu;
using iucs.lms.application.DTOs.Role;
using iucs.lms.application.DTOs.Permission;
using iucs.lms.application.DTOs.Board;
using iucs.lms.application.DTOs.Class;
using iucs.lms.application.DTOs.Subject;
using iucs.lms.application.DTOs.Topic;
using iucs.lms.application.DTOs.Batch;
using iucs.lms.application.DTOs.Course;
using iucs.lms.application.DTOs.CourseContent;
using iucs.lms.application.DTOs.LiveSession;
using iucs.lms.application.DTOs.Payment;
using iucs.lms.application.DTOs.Quiz;

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

        CreateMap<Board, BoardDto>().ReverseMap();
        CreateMap<CreateBoardDto, Board>().ReverseMap();
        CreateMap<UpdateBoardDto, Board>().ReverseMap();

        CreateMap<Class, ClassDto>().ReverseMap();
        CreateMap<CreateClassDto, Class>().ReverseMap();
        CreateMap<UpdateClassDto, Class>().ReverseMap();

        CreateMap<Subject, SubjectDto>().ReverseMap();
        CreateMap<CreateSubjectDto, Subject>().ReverseMap();
        CreateMap<UpdateSubjectDto, Subject>().ReverseMap();

        CreateMap<Topic, TopicDto>().ReverseMap();
        CreateMap<CreateTopicDto, Topic>().ReverseMap();
        CreateMap<UpdateTopicDto, Topic>().ReverseMap();

        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<CreateCourseDto, Course>().ReverseMap();
        CreateMap<UpdateCourseDto, Course>().ReverseMap();

        CreateMap<CourseContent, CourseContentDto>().ReverseMap();
        CreateMap<CreateCourseContentDto, CourseContent>().ReverseMap();

        CreateMap<Batch, BatchDto>().ReverseMap();
        CreateMap<CreateBatchDto, Batch>().ReverseMap();

        CreateMap<LiveSession, LiveSessionDto>().ReverseMap();
        CreateMap<CreateLiveSessionDto, LiveSession>().ReverseMap();

        CreateMap<CreateQuizDto, Quiz>().ReverseMap();
        CreateMap<Quiz, QuizDto>().ReverseMap();

        CreateMap<CreateQuizQuestionDto, QuizQuestion>().ReverseMap();

        CreateMap<QuizAttemptDto, QuizAttempt>().ReverseMap();

        CreateMap<SubscribeDto, Subscription>().ReverseMap();

        CreateMap<CreatePaymentDto, PaymentTransaction>().ReverseMap();

        CreateMap<CreateRefundDto, RefundRequest>().ReverseMap();
    }
}
