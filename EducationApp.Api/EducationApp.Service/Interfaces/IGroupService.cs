using EducationApp.Service.Dtos.Common;
using EducationApp.Service.Dtos.GroupDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationApp.Service.Interfaces
{
    public interface IGroupService
    {
        CreatedResultDto Post(GroupPostDto postDto);
        void Put(int id, GroupPutDto putDto);
        GroupGetDto GetById(int id);
        List<GroupGetAllDto> GetAll();
        void Delete(int id);
    }
}
