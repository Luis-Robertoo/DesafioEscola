using DesafioEscola.Application.Services;

namespace DesafioEscola.Tests.Application.Services;

public class CryptServiceTests
{
    [Fact]
    public void Deve_Criptografar_Senha_com_Sucesso()
    {
        //Arrange
        var criptor = new CryptService();
        var passDecrpt = "Senha@1234";

        //Act
        var result = criptor.EncryptPassword(passDecrpt);

        //Assert
        Assert.NotNull(result);
        Assert.Equal<string>("5395546DFB06C37BC99889ACCE742B0BD5E1D48916AD4C9AFA120A884C43966A", result);
    }
}
