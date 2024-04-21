using DesafioEscola.Application.DTO;
using DesafioEscola.Application.Validators;
using FluentValidation;

namespace DesafioEscola.Tests.Application.Validators;

public class RegisterStudentDTOTests
{
    private readonly RegisterStudentDTOValidator _validator;

    public RegisterStudentDTOTests()
    {
        _validator = new RegisterStudentDTOValidator();
    }

    [Fact]
    public void Deve_Retornar_Sucesso()
    {
        //Arrange
        var dto = new RegisterStudentDTO { Name = "James", Password = "Senha@1234", User = "James42", Active = true };

        //Act
        var result = _validator.Validate(dto);

        //Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Deve_Validar_Tamanho_Do_Nome()
    {
        //Arrange
        var dto = new RegisterStudentDTO { Name = "Jam", User = "James", Password = "Senha@1234", Active = true };

        //Act
        var result = _validator.Validate(dto);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal("O nome deve ter no minimo 4 caracteres", result.Errors.FirstOrDefault(e => e.ErrorMessage.Contains("no minimo 4")).ErrorMessage);
    }

    [Fact]
    public void Deve_Validar_Se_Nome_E_Nulo()
    {
        //Arrange
        var dto = new RegisterStudentDTO { Name = null, User = "James", Password = "Senha@1234", Active = true };

        //Act
        var result = _validator.Validate(dto);

        //Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Where(e => e.ErrorMessage.Contains("não pode ser nulo")).Count() == 1);
    }

    [Fact]
    public void Deve_Validar_Tamanho_Do_Usuario()
    {
        //Arrange
        var dto = new RegisterStudentDTO { Name = "James", User = "Jam", Password = "Senha@1234", Active = true };

        //Act
        var result = _validator.Validate(dto);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal("O campo usuario deve ter no minimo 4 caracteres", result.Errors.FirstOrDefault(e => e.ErrorMessage.Contains("no minimo 4")).ErrorMessage);
    }

    [Fact]
    public void Deve_Validar_Se_Usuario_E_Nulo()
    {
        //Arrange
        var dto = new RegisterStudentDTO { Name = "James", User = null, Password = "Senha@1234", Active = true };

        //Act
        var result = _validator.Validate(dto);

        //Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Where(e => e.ErrorMessage.Contains("não pode ser nulo")).Count() == 1);
    }

    [Fact]
    public void Deve_Validar_Se_Senha_Esta_Vazio()
    {
        //Arrange
        var dto = new RegisterStudentDTO { Name = "James", User = "James", Password = "", Active = true };

        //Act
        var result = _validator.Validate(dto);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal("A senha não pode estar vazia", result.Errors.FirstOrDefault(e => e.ErrorMessage.Contains("pode estar vazia")).ErrorMessage);
    }

    [Fact]
    public void Deve_Validar_Se_Senha_E_Nulo()
    {
        //Arrange
        var dto = new RegisterStudentDTO { Name = "James", User = "James", Password = null, Active = true };

        //Act
        var result = _validator.Validate(dto);

        //Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Where(e => e.ErrorMessage.Contains("não pode ser nula")).Count() == 1);
    }

    [Fact]
    public void Deve_Validar_Se_Senha_Menor__Que_O_Minino()
    {
        //Arrange
        var dto = new RegisterStudentDTO { Name = "James", User = "James", Password = "Se@1", Active = true };

        //Act
        var result = _validator.Validate(dto);

        //Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Where(e => e.ErrorMessage.Contains("A senha deve ter no minimo 8 caracteres")).Count() == 1);
    }

    [Fact]
    public void Deve_Validar_Se_Senha_Maior_Que_O_Maximo()
    {
        //Arrange
        var dto = new RegisterStudentDTO { Name = "James", User = "James", Password = "Senha@1234789", Active = true };

        //Act
        var result = _validator.Validate(dto);

        //Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Where(e => e.ErrorMessage.Contains("A senha deve ter no maximo 10 caracteres")).Count() == 1);
    }

    [Fact]
    public void Deve_Validar_Se_Senha_E_Segura()
    {
        //Arrange
        var dto = new RegisterStudentDTO { Name = "James", User = "James", Password = "12345678", Active = true };

        //Act
        var result = _validator.Validate(dto);

        //Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Where(e => e.ErrorMessage.Contains("A senha deve conter ao menos 1 letra maiuscula, 1 letra minuscula, 1 numero e um dos seguintes caracteres especiais '$*&@#'")).Count() == 1);
    }
}
