namespace DesafioEscola.Domain.Entities;

public class AlunoTurma : BaseModel
{
    public int Aluno_Id { get; set; }
    public int Turma_Id { get; set; }
    public bool Ativo { get; set; }
}
