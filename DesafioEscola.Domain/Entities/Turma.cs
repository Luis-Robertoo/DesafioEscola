namespace DesafioEscola.Domain.Entities;

public class Turma : BaseModel
{
    public int Curso_Id { get; set; }
    public int Ano { get; set; }
    public string Nome { get; set; }
    public bool Ativo { get; set; }
}
