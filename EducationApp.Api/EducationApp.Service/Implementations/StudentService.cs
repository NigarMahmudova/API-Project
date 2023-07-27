using AutoMapper;
using EducationApp.Core.Entities;
using EducationApp.Core.Repositories;
using EducationApp.Service.Dtos.Common;
using EducationApp.Service.Dtos.StudentDtos;
using EducationApp.Service.Exceptions;
using EducationApp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EducationApp.Service.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IGroupRepository groupRepository, IStudentRepository studentRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public List<StudentGetAllDto> GetAll()
        {
            var dtos = _studentRepository.GetQueryable(x=>true, "Brand").ToList();

            return _mapper.Map<List<StudentGetAllDto>>(dtos);
        }

        public StudentGetDto GetById(int id)
        {
            var entity = _studentRepository.Get(x=>x.Id == id, "Group");

            if(entity == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Student not found by id:{id}");

            return _mapper.Map<StudentGetDto>(entity);
        }

        public CreatedResultDto Post(StudentPostDto postDto)
        {
            if(!_groupRepository.IsExist(x=>x.Id == postDto.GroupId))
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "GroupId", $"Group not found by id: {postDto.GroupId}");
            }

            var entity = _mapper.Map<Student>(postDto);

            _studentRepository.Add(entity);
            _studentRepository.Commit();

            return new CreatedResultDto { Id = entity.Id };
        }

        public void Put(int id, StudentPutDto putDto)
        {
            var entity = _studentRepository.Get(x => x.Id == id);

            if (entity == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Student not found by id: {id}");

            if (entity.GroupId != putDto.GroupId && !_groupRepository.IsExist(x => x.Id == putDto.GroupId))
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "GroupId", "BrandId not found");

            entity.FullName = putDto.FullName;
            entity.Point = putDto.Point;
            entity.GroupId = putDto.GroupId;

            _studentRepository.Commit();
        }

        public void Delete(int id)
        {
            var entity = _studentRepository.Get(x => x.Id == id);

            if (entity == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"Student not found by id: {id}");

            _studentRepository.Remove(entity);
            _studentRepository.Commit();
        }

    }
}
