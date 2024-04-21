namespace DesafioEscola.Application.DTO;

public class CreateClassroomDTO
{
    public int Course_Id { get; set; }
    public int Year { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; } = true;
}
