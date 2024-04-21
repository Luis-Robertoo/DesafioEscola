using DesafioEscola.Application.DTO;
using DesafioEscola.Application.Interfaces;
using DesafioEscola.Application.Services;
using Microsoft.Extensions.Configuration;
using Moq;

namespace DesafioEscola.Tests.Application.Services;

public class AuthenticationServiceTests
{
    private readonly IConfiguration _configuration;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly Mock<ICryptService> _cryptServiceMock;
    private readonly Mock<IStudentService> _studentServiceMock;

    private readonly IAuthenticationService _authenticationService;

    public AuthenticationServiceTests()
    {
        var inMemorySettings = new Dictionary<string, string> {
            {"SecretJWT", "af42ghaf42ghaf42ghaf42ghtyoutrtyoutrtyoutrtyoutr"}
        };

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _tokenServiceMock = new Mock<ITokenService>();
        _cryptServiceMock = new Mock<ICryptService>();
        _studentServiceMock = new Mock<IStudentService>();

        _authenticationService = new AuthenticationService(_configuration, _tokenServiceMock.Object, _cryptServiceMock.Object, _studentServiceMock.Object);
    }

    [Fact]
    public async Task Deve_Registrar_Aluno_Com_Sucesso()
    {
        //Arrange
        var registerStudent = new RegisterStudentDTO { Name = "James", Password = "Senha@1234", User = "James42", Active = true };
        var student = new StudentDTO { Name = "James", User = "James42", Active = true, Id = 1 };

        _cryptServiceMock.Setup(x => x.EncryptPassword(It.IsAny<string>())).Returns(It.IsAny<string>());
        _studentServiceMock.Setup(x => x.Create(registerStudent)).ReturnsAsync(student);

        //Act
        var result = await _authenticationService.Register(registerStudent);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(registerStudent.User, result.User);
    }

    [Fact]
    public async Task Deve_Logar_Com_Sucesso()
    {
        //Arrange
        var login = new LoginStudentDTO { Password = "Senha@1234", User = "James42" };

        _cryptServiceMock.Setup(x => x.EncryptPassword(It.IsAny<string>())).Returns(It.IsAny<string>());
        _studentServiceMock.Setup(x => x.GetByUserPassword(login.User, login.Password)).ReturnsAsync(It.IsAny<StudentDTO>());
        _tokenServiceMock.Setup(x => x.GenerateToken(It.IsAny<StudentDTO>(), It.IsAny<IConfiguration>())).Returns("token4789652");

        //Act
        var result = await _authenticationService.Login(login);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<string>(result);
    }

}
