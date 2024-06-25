
using AutoMapper;
using UniVerServer.Events.Dto;
using UniVerServer.Events.Models;
using UniVerServer.Subjects.DTO;

namespace UniVerServer.Events.Mapping;

public class EventsMapping : Profile
{
    public EventsMapping()
    {
        CreateMap<CreateEventDto, Event>();
        CreateMap<Event, GetEventsDto>();
        CreateMap<Users.Models.Users, LecturerInformation>()
            .ForMember(dest => dest.LecturerId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullNames, opt => opt.MapFrom(src => src.FirstNames + " " + src.LastNames))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.IssuedEmail))
            .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src => src.ProfileImage))
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active));
        CreateMap<Event, GetSingleEventDto>()
            .ForMember(dest => dest.Lecturer, opt => opt.MapFrom(src => src.Organiser));

        CreateMap<Event, UpdateEventDto>();
        CreateMap<UpdateEventDto, Event>();
    }
}