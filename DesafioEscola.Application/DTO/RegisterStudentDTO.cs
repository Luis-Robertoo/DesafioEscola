namespace DesafioEscola.Application.DTO
{
    public class RegisterStudentDTO
    {
        public string Name { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; } = true;
    }
}
