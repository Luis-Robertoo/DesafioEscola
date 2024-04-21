namespace DesafioEscola.Application.DTO;

public class ClassroomDTO
{
    public int Id { get; set; }
    public int Course_Id { get; set; }
    public int Year { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; } = true;
}
