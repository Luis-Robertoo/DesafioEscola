namespace DesafioEscola.Application.DTO;

public class StudentClassroomDTO
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int ClassroomId { get; set; }
    public bool Active { get; set; } = true;
}
