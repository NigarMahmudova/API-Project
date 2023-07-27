using EducationApi.Data;
using EducationApp.Service.Dtos.StudentDtos;
using EducationApp.Core.Entities;
using EducationApp.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EducationApp.Service.Interfaces;

namespace EducationApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("{id}")]
        public ActionResult<StudentGetDto> Get(int id)
        {
            return Ok(_studentService.GetById(id));
        }

        [HttpGet("all")]
        public ActionResult<List<StudentGetAllDto>> GetAll()
        {
            return Ok(_studentService.GetAll());
        }

        [HttpPost("")]
        public IActionResult Post(StudentPostDto studentDto)
        {
            return StatusCode(201, _studentService.Post(studentDto));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, StudentPutDto studentDto)
        {
            _studentService.Put(id, studentDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _studentService.Delete(id);

            return NoContent();
        }

    }
}
