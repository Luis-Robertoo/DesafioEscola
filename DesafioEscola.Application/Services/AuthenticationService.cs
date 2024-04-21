using DesafioEscola.Application.DTO;
using DesafioEscola.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DesafioEscola.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;
    private readonly ICryptService _cryptService;
    private readonly IStudentService _studentService;

    public AuthenticationService(IConfiguration configuration, ITokenService tokenService,ICryptService cryptService, IStudentService studentService)
    {
        _configuration = configuration;
        _tokenService = tokenService;
        _cryptService = cryptService;
        _studentService = studentService;   
    }

    public async Task<StudentDTO> Register(RegisterStudentDTO dto)
    {
        dto.Password = _cryptService.EncryptPassword(dto.Password);
        var studentCreated = await _studentService.Create(dto);
        return studentCreated;
    }

    public async Task<string> Login(LoginStudentDTO dto)
    {
        var passwordEncrypt = _cryptService.EncryptPassword(dto.Password);
        var student = await _studentService.GetByUserPassword(dto.User, passwordEncrypt);
        var token = _tokenService.GenerateToken(student, _configuration);
        return token;
    }
}
