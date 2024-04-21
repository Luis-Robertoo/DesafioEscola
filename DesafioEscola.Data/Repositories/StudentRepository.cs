using Dapper;
using DesafioEscola.Crosscutting.Helpers;
using DesafioEscola.Data.Context;
using DesafioEscola.Data.Interfaces;
using DesafioEscola.Domain.Entities;
using System.Net;

namespace DesafioEscola.Data.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly DbSession _dbSession;
    public StudentRepository(DbSession dbSession)
    {
        _dbSession = dbSession;
    }
    public async Task<Aluno?> GetById(int id)
    {
        var result = await _dbSession.Connection.QueryFirstOrDefaultAsync<Aluno>("SELECT * FROM Aluno WHERE Id = @Id", new { Id = id });
        return result;
    }

    public async Task<Aluno?> GetByName(string name)
    {
        var result = await _dbSession.Connection.QueryFirstOrDefaultAsync<Aluno>("SELECT * FROM Aluno WHERE Nome LIKE" + "'%" + "@Nome" + "%'", new { Nome = name });
        return result;
    }

    public async Task<Aluno?> GetByUser(string user)
    {
        var result = await _dbSession.Connection.QueryFirstOrDefaultAsync<Aluno>("SELECT * FROM Aluno WHERE Usuario = @Usuario", new { Usuario = user });
        return result;
    }

    public async Task<Aluno?> GetByUserPassword(string user, string password)
    {
        var result = await _dbSession.Connection.QueryFirstOrDefaultAsync<Aluno>("SELECT * FROM Aluno WHERE Usuario = @Usuario and Senha = @Senha", new { Usuario = user, Senha = password });
        return result;
    }

    public async Task<IEnumerable<Aluno>> GetAll()
    {
        var result = await _dbSession.Connection.QueryAsync<Aluno>("SELECT * FROM Aluno");
        return result;
    }

    public async Task<Aluno> Create(Aluno aluno)
    {
        var result = await _dbSession.Connection.ExecuteAsync("INSERT INTO Aluno (Nome, Usuario, Senha, Ativo) VALUES (@Nome, @Usuario, @Senha, @Ativo)", aluno);
        if(result != 1) throw new ExceptionAPI(DefaultMessages.ERRO_AO_CRIAR_ALUNO, HttpStatusCode.InsufficientStorage);

        var studentCreated = await _dbSession.Connection.QueryFirstAsync<Aluno>("SELECT * FROM Aluno WHERE Nome = @Nome and Usuario = @Usuario and Senha = @Senha and Ativo = @Ativo", aluno);
        return studentCreated;
    }

    public async Task<Aluno> Update(Aluno aluno)
    {
        var result = await _dbSession.Connection.ExecuteAsync("UPDATE Aluno SET Nome = @Nome, Usuario = @Usuario, Senha = @Senha, Ativo = @Ativo Where Id = @Id", aluno);
        if (result != 1) throw new ExceptionAPI(DefaultMessages.ERRO_AO_CRIAR_ALUNO, HttpStatusCode.InsufficientStorage);

        var studentCreated = await _dbSession.Connection.QueryFirstAsync<Aluno>("SELECT * FROM Aluno WHERE Nome = @Nome and Usuario = @Usuario and Senha = @Senha and Ativo = @Ativo", aluno);
        return studentCreated;
    }

    public async Task<bool> Delete(int id)
    {
        var result = await _dbSession.Connection.ExecuteAsync("UPDATE Aluno SET Ativo = 0 Where Id = @Id", new { Id = id });
        if(result == 1) return true;

        return false;
    }
}
