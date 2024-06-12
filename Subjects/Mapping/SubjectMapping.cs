using AutoMapper;
using UniVerServer.Subjects.DTO;
using UniVerServer.Subjects.Models;

namespace UniVerServer.Subjects.Mapping;

public class SubjectMapping: Profile
{
    public SubjectMapping()
    {
        // From -> To?
        CreateMap<CreateSubjectDto, Subject>();
        CreateMap<Subject, GetSubjectDto>();
        CreateMap<UpdateSubjectDto, Subject>();
        CreateMap<Users.Models.Users, LecturerInformation>()
            .ForMember(dest => dest.LecturerId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullNames, opt => opt.MapFrom(src => src.FirstNames + " " + src.LastNames))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.IssuedEmail))
            .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src => src.ProfileImage))
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active));
        CreateMap<Subject, GetSingleSubjectDto>()
            .ForMember(dest => dest.Lecturer, opt => opt.MapFrom(src => src.Lecturer));
    }
}