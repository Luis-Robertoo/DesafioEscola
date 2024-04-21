using AutoMapper;
using DesafioEscola.Application.DTO;
using DesafioEscola.Application.Interfaces;
using DesafioEscola.Crosscutting.Helpers;
using DesafioEscola.Data.Interfaces;
using DesafioEscola.Domain.Entities;
using System.Net;

namespace DesafioEscola.Application.Services;

public class StudentClassRoomService : IStudentClassRoomService
{
    private readonly IStudentClassRoomRepository _studentClassRoomRepository;
    private readonly IMapper _mapper;

    public StudentClassRoomService(IMapper mapper, IStudentClassRoomRepository studentClassRoomRepository)
    {
        _mapper = mapper;
        _studentClassRoomRepository = studentClassRoomRepository;
    }

    public async Task<IEnumerable<StudentClassroomDTO>> GetByClassroomId(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<StudentClassroomDTO>> GetByStudentId(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<StudentDTO>> GetStudentsByClassroomId(int id)
    {
        var students = await _studentClassRoomRepository.GetStudentsByClassroomId(id);
        return _mapper.Map<IEnumerable<StudentDTO>>(students);
    }

    public async Task<IEnumerable<StudentDTO>> Create(CreateStudentClassroomDTO dto)
    {
        await ExistBond(dto.ClassroomId, dto.StudentId);

        var studentClassroom = _mapper.Map<AlunoTurma>(dto);
        await _studentClassRoomRepository.Create(studentClassroom);

        return await GetStudentsByClassroomId(dto.ClassroomId);
    }

    public async Task<IEnumerable<StudentDTO>> Update(StudentClassroomDTO dto)
    {
        await ExistBond(dto.ClassroomId, dto.StudentId, dto.Id);

        var studentClassroom = _mapper.Map<AlunoTurma>(dto);
        await _studentClassRoomRepository.Update(studentClassroom);

        return await GetStudentsByClassroomId(dto.ClassroomId);
    }

    public async Task<bool> Delete(int id)
    {
        var result = await _studentClassRoomRepository.Delete(id);
        return result;
    }

    private async Task ExistBond(int classroomId, int studentId, int? id = null)
    {
        var StudentClassroomSaved = await _studentClassRoomRepository.GetByClassroomIdAndStudentId(classroomId, studentId);
        if (StudentClassroomSaved != null && (id is null || id != StudentClassroomSaved.Id)) throw new ExceptionAPI(DefaultMessages.ALUNO_JA_VINCULA_A_ESTA_TURMA, HttpStatusCode.BadRequest);
    }
}
