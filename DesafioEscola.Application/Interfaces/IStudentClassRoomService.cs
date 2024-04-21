using DesafioEscola.Application.DTO;

namespace DesafioEscola.Application.Interfaces;

public interface IStudentClassRoomService
{
    Task<IEnumerable<StudentClassroomDTO>> GetByClassroomId(int id);
    Task<IEnumerable<StudentClassroomDTO>> GetByStudentId(int id);
    Task<IEnumerable<StudentDTO>> GetStudentsByClassroomId(int id);
    Task<IEnumerable<StudentDTO>> Create(CreateStudentClassroomDTO studentClassroom);
    Task<IEnumerable<StudentDTO>> Update(StudentClassroomDTO studentClassroom);
    Task<bool> Delete(int id);
}
