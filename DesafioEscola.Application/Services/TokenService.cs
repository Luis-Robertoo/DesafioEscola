using DesafioEscola.Application.DTO;
using DesafioEscola.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DesafioEscola.Application.Services;

public class TokenService : ITokenService
{
    public string GenerateToken(StudentDTO dto, IConfiguration configuration)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secret = configuration.GetValue<string>("SecretJWT");
        var key = Encoding.ASCII.GetBytes(secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim("Nome", dto.Name),
                    new Claim("Usuario", dto.User),
                    new Claim("Id", dto.Id.ToString()),
                    new Claim(ClaimTypes.Role, dto.User)

            }),
            Expires = DateTime.UtcNow.AddHours(24),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
