using AutoMapper;
using TextRepo.API.DataTransferObjects;
using TextRepo.Commons.Models;
namespace TextRepo.API
{
    /// <summary>
    /// Automapper settings
    /// </summary>
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.Type,
                    opt => 
                        opt.MapFrom(src => src.ContactInfo.Type))
                .ForMember(dest => dest.Value,
                    opt => 
                        opt.MapFrom(src => src.ContactInfo.Value));
            
            CreateMap<Project, ProjectResponseDto>();
            CreateMap<Document, DocumentResponseDto>();
            CreateMap<ContactInfo, ContactInfoResponseDto>();
            CreateMap<UserRequestDto, User>()
                .ForMember(dest => dest.HashedPassword,
                    opt =>
                    {
                        opt.MapFrom(src => src.Password);
                        opt.NullSubstitute("");
                    });
            CreateMap<ProjectRequestDto, Project>();
            CreateMap<DocumentRequestDto, Document>();
            CreateMap<ContactInfoRequestDto, ContactInfo>()
                .ForMember(dest => dest.Type,
                    opt => opt.NullSubstitute(""))
                .ForMember(dest => dest.Value,
                    opt => opt.NullSubstitute(""));
        }
    }
}