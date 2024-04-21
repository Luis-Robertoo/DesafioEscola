using AutoMapper;
using DesafioEscola.Application.DTO;
using DesafioEscola.Application.Interfaces;
using DesafioEscola.Crosscutting.Helpers;
using DesafioEscola.Data.Interfaces;
using DesafioEscola.Domain.Entities;
using System.Net;

namespace DesafioEscola.Application.Services
{
    public class ClassroomService : IClassroomService
    {

        private readonly IClassroomRepository _classroomRepository;
        private readonly IMapper _mapper;

        public ClassroomService(IClassroomRepository classroomRepository, IMapper mapper) 
        {
            _classroomRepository = classroomRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClassroomDTO>> GetAll()
        {
            var classrooms = await _classroomRepository.GetAll();
            var classroomsDTO = _mapper.Map<IEnumerable<ClassroomDTO>>(classrooms);
            return classroomsDTO;
        }

        public async Task<ClassroomDTO> GetById(int id)
        {
            var classroom = await _classroomRepository.GetById(id);
            if (classroom is null) return null;
            return _mapper.Map<ClassroomDTO>(classroom);
        }

        public async Task<ClassroomDTO> GetByName(string name)
        {
            var classroom = await _classroomRepository.GetByName(name);
            if (classroom is null) return null;
            return _mapper.Map<ClassroomDTO>(classroom);
        }
        public async Task<ClassroomDTO> Create(CreateClassroomDTO dto)
        {
            CheckDate(dto.Year);
            await CheckClassroomName(dto.Name);

            var classroom = _mapper.Map<Turma>(dto);
            var classroomCreated = await _classroomRepository.Create(classroom);
            return _mapper.Map<ClassroomDTO>(classroomCreated);
        }
        public async Task<ClassroomDTO> Update(ClassroomDTO dto)
        {
            CheckDate(dto.Year);
            await CheckClassroomName(dto.Name, dto.Id);

            var classroom = _mapper.Map<Turma>(dto);
            var classroomStudent = await _classroomRepository.Update(classroom);
            return _mapper.Map<ClassroomDTO>(classroomStudent);
        } 
        public async Task<bool> Delete(int id)
        {
            var result = await _classroomRepository.Delete(id);
            return result;
        }

        private static void CheckDate(int year)
        {
            var dateNow = DateTime.Now.Year;
            if (dateNow > year) throw new ExceptionAPI(DefaultMessages.ANO_DA_TURMA_INVALIDO, HttpStatusCode.BadRequest);
        }

        private async Task CheckClassroomName(string name, int? id = null )
        {
            var classroomSaved = await _classroomRepository.GetByName(name);
            if (classroomSaved != null && (id is null || id != classroomSaved.Id)) throw new ExceptionAPI(DefaultMessages.NOME_DA_TURMA_NAO_DISPONIVEL, HttpStatusCode.BadRequest);
        }
    }
}
