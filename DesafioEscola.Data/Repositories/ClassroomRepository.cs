using Dapper;
using DesafioEscola.Crosscutting.Helpers;
using DesafioEscola.Data.Context;
using DesafioEscola.Data.Interfaces;
using DesafioEscola.Domain.Entities;
using System.Net;

namespace DesafioEscola.Data.Repositories;

public class ClassroomRepository : IClassroomRepository
{

    private readonly DbSession _dbSession;
    public ClassroomRepository(DbSession dbSession)
    {
        _dbSession = dbSession;
    }
    public async Task<IEnumerable<Turma>> GetAll()
    {
        var result = await _dbSession.Connection.QueryAsync<Turma>("SELECT * FROM Turma");
        return result;
    }

    public async Task<Turma?> GetById(int id)
    {
        var result = await _dbSession.Connection.QueryFirstOrDefaultAsync<Turma>("SELECT * FROM Turma WHERE Id = @Id", new { Id = id });
        return result;
    }

    public async Task<Turma?> GetByName(string name)
    {
        var result = await _dbSession.Connection.QueryFirstOrDefaultAsync<Turma>("SELECT * FROM Turma WHERE Nome = @Nome", new { Nome = name });
        return result;
    }

    public async Task<Turma?> Create(Turma turma)
    {
        var result = await _dbSession.Connection.ExecuteAsync("INSERT INTO Turma (Nome, Ano, Curso_Id, Ativo) VALUES (@Nome, @Ano, @Curso_Id, @Ativo)", turma);
        if (result != 1) throw new ExceptionAPI(DefaultMessages.ERRO_AO_CRIAR_TURMA, HttpStatusCode.InsufficientStorage);

        var classCreated = await _dbSession.Connection.QueryFirstAsync<Turma>("SELECT * FROM Turma WHERE Nome = @Nome and Ano = @Ano and Curso_Id = @Curso_Id and Ativo = @Ativo", turma);
        return classCreated;
    }
    public async Task<Turma?> Update(Turma turma)
    {
        var result = await _dbSession.Connection.ExecuteAsync("UPDATE Turma SET Nome = @Nome, Ano = @Ano, Curso_Id = @Curso_Id, Ativo = @Ativo Where Id = @Id", turma);
        if (result != 1) throw new ExceptionAPI(DefaultMessages.ERRO_AO_CRIAR_TURMA, HttpStatusCode.InsufficientStorage);

        var classUpdated = await _dbSession.Connection.QueryFirstAsync<Turma>("SELECT * FROM Turma WHERE Nome = @Nome and Ano = @Ano and Curso_Id = @Curso_Id and Ativo = @Ativo", turma);
        return classUpdated;
    }
    public async Task<bool> Delete(int id)
    {
        var result = await _dbSession.Connection.ExecuteAsync("UPDATE Turma SET Ativo = 0 Where Id = @Id", new { Id = id });
        if (result == 1) return true;

        return false;
    }
}
