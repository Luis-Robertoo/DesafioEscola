using DesafioEscola.Application.DTO;
using DesafioEscola.Application.Services;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace DesafioEscola.Tests.Application.Services;

public class TokenServiceTests
{ 

    [Fact]
    public void Deve_Gerar_Token_com_Sucesso()
    {
        //Arrange
        var generator = new TokenService();
        var data = new StudentDTO { Id = 1, Name = "James", User = "ADMIN", Active = true };

        var inMemorySettings = new Dictionary<string, string> {
            {"SecretJWT", "af42ghaf42ghaf42ghaf42ghtyoutrtyoutrtyoutrtyoutr"}
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        //Act
        var result = generator.GenerateToken(data, configuration);

        //Assert
        var handler = new JwtSecurityTokenHandler();
        var claims = handler.ReadJwtToken(result).Claims;

        Assert.NotNull(claims);
        Assert.Equal<string>(data.User, claims.FirstOrDefault( c => c.Type == "Usuario").Value);
    }
}
