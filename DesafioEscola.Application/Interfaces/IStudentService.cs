using DesafioEscola.Application.DTO;

namespace DesafioEscola.Application.Interfaces;

public interface IStudentService
{
    Task<StudentDTO> GetByName(string name);
    Task<StudentDTO> GetById(int id);
    Task<StudentDTO> GetByUser(string user);
    Task<StudentDTO> GetByUserPassword(string user, string password);
    Task<IEnumerable<StudentDTO>> GetAll();
    Task<StudentDTO> Create(RegisterStudentDTO student);
    Task<StudentDTO> Update(UpdateStudentDTO student);
    Task<bool> Delete(int id);
}
