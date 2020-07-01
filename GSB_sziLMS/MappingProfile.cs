using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSB_sziLMS
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Course, CourseDto>();
            CreateMap<Section, SectionDto>();
            CreateMap<Assignment, AssignmentDto>();
            CreateMap<Enrollment, EnrollmentDto>();
            CreateMap<Submission, SubmissionDto>();

        }
    }
}
