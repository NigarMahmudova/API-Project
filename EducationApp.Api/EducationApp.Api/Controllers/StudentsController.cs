using EducationApi.Data;
using EducationApp.Api.Dtos.StudentDtos;
using EducationApp.Core.Entities;
using EducationApp.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGroupRepository _groupRepository;

        public StudentsController(IStudentRepository studentRepository, IGroupRepository groupRepository)
        {
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
        }

        [HttpGet("{id}")]
        public ActionResult<StudentGetDto> Get(int id)
        {
            Student student = _studentRepository.Get(x=>x.Id == id, "Group");

            if(student == null) return NotFound();

            StudentGetDto studentDto = new StudentGetDto
            {
                FullName = student.FullName,
                Point = student.Point,
                Group = new GroupInStudentGetDto
                {
                    Id = student.GroupId,
                    No = student.Group.No,
                }
            };

            return Ok(studentDto);
        }

        [HttpGet("all")]
        public ActionResult<List<StudentGetAllDto>> GetAll()
        {
            var studentDtos = _studentRepository.GetQueryable(x => true, "Group").Select(x => new StudentGetAllDto
            {
                Id = x.Id,
                FullName = x.FullName,
                Point = x.Point,
                GroupNo = x.Group.No
            }).ToList();

            return Ok(studentDtos);
        }

        [HttpPost("")]
        public IActionResult Post(StudentPostDto studentDto)
        {
            if (!_groupRepository.IsExist(x => x.Id == studentDto.GroupId))
            {
                ModelState.AddModelError("GroupId", $"Group not found by id: {studentDto.GroupId}");
                return BadRequest(ModelState);
            }

            Student student = new Student
            {
                GroupId = studentDto.GroupId,
                FullName = studentDto.FullName,
                Point = studentDto.Point,
            };

            _studentRepository.Add(student);
            _studentRepository.Commit();

            return StatusCode(201, new { Id = student.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, StudentPutDto studentDto)
        {
            Student student = _studentRepository.Get(x=>x.Id == id);

            if(student == null) return NotFound();

            student.GroupId = studentDto.GroupId;
            student.FullName = studentDto.FullName;
            student.Point = studentDto.Point;

            _studentRepository.Commit();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Student student = _studentRepository.Get(x=>x.Id == id);

            if(student == null) return NotFound();

            _studentRepository.Remove(student);
            _studentRepository.Commit();

            return NoContent();
        }

    }
}
