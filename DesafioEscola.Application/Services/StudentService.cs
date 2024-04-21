using AutoMapper;
using DesafioEscola.Application.DTO;
using DesafioEscola.Application.Interfaces;
using DesafioEscola.Crosscutting.Helpers;
using DesafioEscola.Data.Interfaces;
using DesafioEscola.Domain.Entities;
using System.Net;

namespace DesafioEscola.Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly ICryptService _cryptService;
    private readonly IMapper _mapper;

    public StudentService(IStudentRepository studentRepository, ICryptService cryptService, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _cryptService = cryptService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StudentDTO>> GetAll()
    {
        var students = await _studentRepository.GetAll();
        var studentsDTO = _mapper.Map<IEnumerable<StudentDTO>>(students);
        return studentsDTO;
    }

    public async Task<StudentDTO> GetById(int id)
    {
        var student = await _studentRepository.GetById(id);
        if (student is null) return null;
        return _mapper.Map<StudentDTO>(student);
    }

    public async Task<StudentDTO> GetByName(string name)
    {
        var student = await _studentRepository.GetByName(name);
        if (student is null) return null; 
        return _mapper.Map<StudentDTO>(student);
    }

    public async Task<StudentDTO> GetByUser(string user)
    {
        var student = await _studentRepository.GetByUser(user);
        if (student is null) return null; 
        return _mapper.Map<StudentDTO>(student);
    }

    public async Task<StudentDTO> GetByUserPassword(string user, string password)
    {
        var student = await _studentRepository.GetByUserPassword(user, password);
        if (student is null) throw new ExceptionAPI(DefaultMessages.USUARIO_OU_SENHA_INVALIDOS, HttpStatusCode.NotFound);
        return _mapper.Map<StudentDTO>(student);
    }

    public async Task<StudentDTO> Create(RegisterStudentDTO dto)
    {
        await CheckUserName(dto.User);

        var student = _mapper.Map<Aluno>(dto);
        var studentCreated = await _studentRepository.Create(student);
        return _mapper.Map<StudentDTO>(studentCreated);
    }

    public async Task<StudentDTO> Update(UpdateStudentDTO updateStudent)
    {
        await CheckUserName(updateStudent.User, updateStudent.Id);

        updateStudent.Password = _cryptService.EncryptPassword(updateStudent.Password);

        var student = _mapper.Map<Aluno>(updateStudent);
        var updatedStudent = await _studentRepository.Update(student);
        return _mapper.Map<StudentDTO>(updatedStudent);
    }

    public async Task<bool> Delete(int id)
    {
        var result = await _studentRepository.Delete(id);
        return result;
    }

    private async Task CheckUserName(string user, int? id = null)
    {
        var studentSaved = await _studentRepository.GetByUser(user);
        if (studentSaved != null && (id is null || studentSaved.Id != id)) throw new ExceptionAPI(DefaultMessages.NOME_DE_USUARIO_NAO_DISPONIVEL, HttpStatusCode.BadRequest);
    }
}
