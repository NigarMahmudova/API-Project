using EducationApp.Service.Dtos.Common;
using EducationApp.Service.Dtos.StudentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationApp.Service.Interfaces
{
    public interface IStudentService
    {
        CreatedResultDto Post(StudentPostDto postDto);
        StudentGetDto GetById(int id);
        List<StudentGetAllDto> GetAll();
        void Put(int id, StudentPutDto putDto);
        void Delete(int id);
    }
}
