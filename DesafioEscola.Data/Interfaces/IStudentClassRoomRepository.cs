using DesafioEscola.Domain.Entities;

namespace DesafioEscola.Data.Interfaces;

public interface IStudentClassRoomRepository
{
    Task<IEnumerable<AlunoTurma>> GetByStudentId(int studentId);
    Task<IEnumerable<AlunoTurma>> GetByClassroomId(int classroomId);
    Task<AlunoTurma?> GetByClassroomIdAndStudentId(int classroomId, int studentId);
    Task<IEnumerable<Aluno>> GetStudentsByClassroomId(int classroomId);
    Task Create(AlunoTurma studentClassRoom);
    Task Update(AlunoTurma studentClassRoom);
    Task<bool> Delete(int id);
}
