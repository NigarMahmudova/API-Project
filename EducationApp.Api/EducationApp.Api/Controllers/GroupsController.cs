using EducationApi.Data;
using EducationApp.Api.Dtos.GroupDtos;
using EducationApp.Core.Entities;
using EducationApp.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IStudentRepository _studentRepository;

        public GroupsController(IGroupRepository groupRepository, IStudentRepository studentRepository)
        {
            _groupRepository = groupRepository;
            _studentRepository = studentRepository;
        }

        [HttpGet("all")]
        public ActionResult<List<GroupGetAllDto>> GetAll()
        {
            var groupDtos = _groupRepository.GetQueryable(x=>true).Select(x => new GroupGetAllDto
            {
                Id = x.Id,
                No = x.No,
            }).ToList();

            return Ok(groupDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<GroupGetDto> Get(int id)
        {
            Group group = _groupRepository.Get(x=>x.Id == id);

            if(group == null) return NotFound();

            GroupGetDto groupDto = new GroupGetDto
            {
                No = group.No,
                StudentsCount = _studentRepository.GetQueryable(x=>x.GroupId == id).Count(),
            };

            return Ok(groupDto);
        }

        [HttpPost("")]
        public IActionResult Post(GroupPostDto groupDto)
        {
            if(_groupRepository.IsExist(x=>x.No == groupDto.No))
            {
                ModelState.AddModelError("No", "No is already taken.");
                return BadRequest(ModelState);
            }

            Group group = new Group
            {
                No = groupDto.No,
            };

            _groupRepository.Add(group);
            _groupRepository.Commit();

            return StatusCode(201, new { Id = group.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, GroupPutDto groupDto)
        {
            Group group = _groupRepository.Get(x=>x.Id == id);

            if(group == null) return NotFound();

            if(group.No != groupDto.No && _groupRepository.IsExist(x=>x.No == groupDto.No))
            {
                ModelState.AddModelError("No", "No is already taken.");
                return BadRequest(ModelState);
            }

            group.No = groupDto.No;
            _groupRepository.Commit();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Group group = _groupRepository.Get(x => x.Id == id);

            if(group == null) return NotFound();

            _groupRepository.Remove(group);
            _groupRepository.Commit();

            return NoContent();
        }
    }
}
