using DesafioEscola.Domain.Entities;

namespace DesafioEscola.Data.Interfaces;

public interface IStudentRepository
{
    Task<Aluno?> GetByName(string name);
    Task<Aluno?> GetById(int id);
    Task<Aluno?> GetByUser(string user);
    Task<Aluno?> GetByUserPassword(string user, string password);
    Task<IEnumerable<Aluno>> GetAll();
    Task<Aluno?> Create(Aluno student);
    Task<Aluno?> Update(Aluno student);
    Task<bool> Delete(int id);
}
