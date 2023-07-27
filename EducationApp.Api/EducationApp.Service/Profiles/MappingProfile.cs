using AutoMapper;
using EducationApp.Core.Entities;
using EducationApp.Service.Dtos.GroupDtos;
using EducationApp.Service.Dtos.StudentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationApp.Service.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<GroupPostDto, Group>();
            CreateMap<Group, GroupPostDto>();
            CreateMap<Group, GroupInStudentGetDto>();
            CreateMap<Group, GroupGetAllDto>();

            CreateMap<StudentPostDto, Student>();
            CreateMap<Student, StudentGetDto>();
            CreateMap<Student, StudentGetAllDto>();
        }
    }
}
