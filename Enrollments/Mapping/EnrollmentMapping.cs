using AutoMapper;
using UniVerServer.Enrollments.DTO;
using UniVerServer.Enrollments.Models;

namespace UniVerServer.Enrollments.Mapping;

public class EnrollmentMapping : Profile
{
 public EnrollmentMapping()
 {
     CreateMap<CreateEnrollmentDto, Enrollment>();
 }
}