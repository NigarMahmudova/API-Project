using AutoMapper;
using EducationApp.Core.Entities;
using EducationApp.Core.Repositories;
using EducationApp.Service.Dtos.Common;
using EducationApp.Service.Dtos.GroupDtos;
using EducationApp.Service.Exceptions;
using EducationApp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationApp.Service.Implementations
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GroupService(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public List<GroupGetAllDto> GetAll()
        {
            var dtos = _groupRepository.GetQueryable(x => true).ToList();

            return _mapper.Map<List<GroupGetAllDto>>(dtos);
        }

        public GroupGetDto GetById(int id)
        {
            var entity = _groupRepository.Get(x=>x.Id == id, "Students");

            if(entity == null)
            {
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Group not found by id: {id}");
            }

            return _mapper.Map<GroupGetDto>(entity);
        }

        public CreatedResultDto Post(GroupPostDto postDto)
        {
            if(_groupRepository.IsExist(x=>x.No == postDto.No))
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "No", "No is already taken");
            }

            var entity = _mapper.Map<Group>(postDto);

            _groupRepository.Add(entity);
            _groupRepository.Commit();

            return new CreatedResultDto { Id = entity.Id };
        }

        public void Put(int id, GroupPutDto putDto)
        {
            var entity = _groupRepository.Get(x => x.Id == id);

            if(entity == null)
            {
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Group not found by id: {id}");
            }

            if(entity.No != putDto.No && _groupRepository.IsExist(x=>x.No == putDto.No))
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "No", "No is already taken");
            }

            entity.No = putDto.No;
            _groupRepository.Commit();
        }

        public void Delete(int id)
        {
            var entity = _groupRepository.Get(x=>x.Id == id);

            if(entity == null)
            {
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Group not found by id: {id}");
            }

            _groupRepository.Remove(entity);
            _groupRepository.Commit();
        }

        
    }
}
