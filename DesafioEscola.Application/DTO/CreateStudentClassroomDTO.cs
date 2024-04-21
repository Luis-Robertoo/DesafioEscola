namespace DesafioEscola.Application.DTO;

public class CreateStudentClassroomDTO
{
    public int StudentId { get; set; }
    public int ClassroomId { get; set; }
    public bool Active { get; set; } = true;
}
