using Dapper;
using DesafioEscola.Crosscutting.Helpers;
using DesafioEscola.Data.Context;
using DesafioEscola.Data.Interfaces;
using DesafioEscola.Domain.Entities;
using System.Net;

namespace DesafioEscola.Data.Repositories;

public class StudentClassRoomRepository : IStudentClassRoomRepository
{
    private readonly DbSession _dbSession;
    public StudentClassRoomRepository(DbSession dbSession)
    {
        _dbSession = dbSession;
    }

    public async Task<IEnumerable<AlunoTurma>> GetByClassroomId(int classroomId)
    {
        var result = await _dbSession.Connection.QueryAsync<AlunoTurma>("SELECT * FROM Aluno_Turma WHERE Turma_Id = @Id", new { Id = classroomId });
        return result;
    }

    public async Task<IEnumerable<AlunoTurma>> GetByStudentId(int studentId)
    {
        var result = await _dbSession.Connection.QueryAsync<AlunoTurma>("SELECT * FROM Aluno_Turma WHERE Aluno_Id = @Id", new { Id = studentId });
        return result;
    }

    public async Task<AlunoTurma?> GetByClassroomIdAndStudentId(int classroomId, int studentId)
    {
        var result = await _dbSession.Connection.QueryFirstOrDefaultAsync<AlunoTurma>("SELECT * FROM Aluno_Turma WHERE Turma_Id = @Turma_Id and Aluno_Id = @Aluno_Id", new { Turma_Id = classroomId, Aluno_Id = studentId });
        return result;
    }

    public  async Task<IEnumerable<Aluno>> GetStudentsByClassroomId(int classroomId)
    {
        var result = await _dbSession.Connection.QueryAsync<Aluno>("SELECT * FROM Aluno a LEFT JOIN Aluno_Turma atu on a.Id = atu.Aluno_Id WHERE atu.Turma_Id = @Id", new { Id = classroomId });
        return result;
    }

    public async Task Create(AlunoTurma studentClassRoom)
    {
        var result = await _dbSession.Connection.ExecuteAsync("INSERT INTO Aluno_Turma (Aluno_Id, Turma_Id, Ativo) VALUES (@Aluno_Id, @Turma_Id, @Ativo)", studentClassRoom);
        if (result != 1) throw new ExceptionAPI(DefaultMessages.ERRO_VINCULAR_ALUNO_A_TURMA, HttpStatusCode.InsufficientStorage);
    }

    public async Task Update(AlunoTurma studentClassRoom)
    {
        var result = await _dbSession.Connection.ExecuteAsync("UPDATE Aluno_Turma SET Aluno_Id = @Aluno_Id, Turma_Id = @Turma_Id, Ativo = @Ativo Where Id = @Id", studentClassRoom);
        if (result != 1) throw new ExceptionAPI(DefaultMessages.ERRO_AO_CRIAR_TURMA, HttpStatusCode.InsufficientStorage);
    }

    public async Task<bool> Delete(int id)
    {
        var result = await _dbSession.Connection.ExecuteAsync("UPDATE Aluno_Turma SET Ativo = 0 Where Id = @Id", new { Id = id });
        if (result == 1) return true;

        return false;
    }
}
