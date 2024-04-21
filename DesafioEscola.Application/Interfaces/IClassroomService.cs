using DesafioEscola.Application.DTO;

namespace DesafioEscola.Application.Interfaces;

public interface IClassroomService
{
    Task<ClassroomDTO> GetByName(string name);
    Task<ClassroomDTO> GetById(int id);
    Task<IEnumerable<ClassroomDTO>> GetAll();
    Task<ClassroomDTO> Create(CreateClassroomDTO classroom);
    Task<ClassroomDTO> Update(ClassroomDTO classroom);
    Task<bool> Delete(int id);
}
