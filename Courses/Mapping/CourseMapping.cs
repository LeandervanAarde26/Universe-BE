using AutoMapper;
using UniVerServer.Courses.DTO;
using UniVerServer.Courses.Models;

namespace UniVerServer.Courses.Mapping;

public class CourseMapping : Profile
{
    public CourseMapping()
    {
        CreateMap<CreateCourseDto, Course>();
        CreateMap<UpdateCourseDto, Course>();

    }
}