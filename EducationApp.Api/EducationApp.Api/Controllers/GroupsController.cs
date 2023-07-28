using EducationApi.Data;
using EducationApp.Service.Dtos.GroupDtos;
using EducationApp.Core.Entities;
using EducationApp.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EducationApp.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace EducationApp.Api.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet("all")]
        public ActionResult<List<GroupGetAllDto>> GetAll()
        {
            return Ok(_groupService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<GroupGetDto> Get(int id)
        {
            return Ok(_groupService.GetById(id));
        }

        [HttpPost("")]
        public IActionResult Post(GroupPostDto groupDto)
        {
            return StatusCode(201, _groupService.Post(groupDto));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, GroupPutDto groupDto)
        {
            _groupService.Put(id, groupDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _groupService.Delete(id);
            return NoContent();
        }
    }
}
