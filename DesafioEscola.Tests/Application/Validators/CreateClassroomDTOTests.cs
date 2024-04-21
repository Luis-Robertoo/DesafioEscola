using DesafioEscola.Application.DTO;
using DesafioEscola.Application.Validators;
using FluentValidation;

namespace DesafioEscola.Tests.Application.Validators;

public class CreateClassroomDTOTests
{
    private readonly CreateClassroomDTOValidator _validator;

    public CreateClassroomDTOTests()
    {
        _validator = new CreateClassroomDTOValidator();
    }

    [Fact]
    public void Deve_Retornar_Sucesso()
    {
        //Arrange
        var dto = new CreateClassroomDTO { Name = "Dados", Active = true, Course_Id = 42, Year = DateTime.Now.AddYears(1).Year };

        //Act
        var result = _validator.Validate(dto);

        //Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Deve_Validar_Tamanho_Do_Nome()
    {
        //Arrange
        var dto = new CreateClassroomDTO { Name = "TI", Active = true, Course_Id = 42, Year = DateTime.Now.AddYears(1).Year };

        //Act
        var result = _validator.Validate(dto);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal("O nome da turma deve ter no minimo 4 caracteres", result.Errors.FirstOrDefault(e => e.ErrorMessage.Contains("no minimo 4")).ErrorMessage);
    }

    [Fact]
    public void Deve_Validar_Se_Nome_E_Vazio()
    {
        //Arrange
        var dto = new CreateClassroomDTO { Name = string.Empty, Active = true, Course_Id = 42, Year = DateTime.Now.AddYears(1).Year };

        //Act
        var result = _validator.Validate(dto);

        //Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Where(e => e.ErrorMessage.Contains("não pode estar vazia")).Count() == 1);
    }

    [Fact]
    public void Deve_Validar_Se_Nome_E_Nulo()
    {
        //Arrange
        var dto = new CreateClassroomDTO { Name = null, Active = true, Course_Id = 42, Year = DateTime.Now.AddYears(1).Year };

        //Act
        var result = _validator.Validate(dto);

        //Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Where(e => e.ErrorMessage.Contains("não pode ser nula")).Count() == 1);
    }

    [Fact]
    public void Deve_Validar_Ano()
    {
        //Arrange
        var dto = new CreateClassroomDTO { Name = "Dados", Active = true, Course_Id = 42, Year = 2020 };

        //Act
        var result = _validator.Validate(dto);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal("Ano da turma invalido, ele precisa ser maior ou igual ao atual.", result.Errors.FirstOrDefault(e => e.ErrorMessage.Contains("Ano da turma invalido")).ErrorMessage);
    }

}
