using DesafioEscola.Application.DTO;

namespace DesafioEscola.Application.Interfaces;

public interface IAuthenticationService
{
    Task<StudentDTO> Register(RegisterStudentDTO dto);

    Task<string> Login(LoginStudentDTO dto);

}
