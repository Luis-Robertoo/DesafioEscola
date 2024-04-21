using DesafioEscola.Application.DTO;
using Microsoft.Extensions.Configuration;

namespace DesafioEscola.Application.Interfaces;

public interface ITokenService 
{
    string GenerateToken(StudentDTO student, IConfiguration configuration);
}
