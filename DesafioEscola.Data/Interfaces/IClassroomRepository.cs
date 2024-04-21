using DesafioEscola.Domain.Entities;

namespace DesafioEscola.Data.Interfaces;

public interface IClassroomRepository
{
    Task<Turma?> GetByName(string name);
    Task<Turma?> GetById(int id);
    Task<IEnumerable<Turma>> GetAll();
    Task<Turma?> Create(Turma turma);
    Task<Turma?> Update(Turma turma);
    Task<bool> Delete(int id);
}
