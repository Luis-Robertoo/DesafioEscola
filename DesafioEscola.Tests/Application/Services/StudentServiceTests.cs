using AutoMapper;
using DesafioEscola.Application.DTO;
using DesafioEscola.Application.Interfaces;
using DesafioEscola.Application.Services;
using DesafioEscola.Crosscutting.Helpers;
using DesafioEscola.Data.Interfaces;
using DesafioEscola.Domain.Entities;
using Moq;

namespace DesafioEscola.Tests.Application.Services;

public class StudentServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IStudentRepository> _studentRepository;
    private readonly Mock<ICryptService> _cryptServiceMock;

    private List<Aluno> _listaAlunos;
    private List<StudentDTO> _listaStudent;

    private readonly IStudentService _studentService;

    public StudentServiceTests() 
    {
        _listaAlunos = new List<Aluno>
        {
            new Aluno { Nome = "James", Usuario = "James", Id = 1, Ativo = true, Senha = "SenhaForte" },
            new Aluno { Nome = "Mary", Usuario = "Mary", Id = 2, Ativo = true, Senha = "SenhaForte" },
            new Aluno { Nome = "Peter", Usuario = "Spider", Id = 3, Ativo = true, Senha = "SenhaForte" },
            new Aluno { Nome = "Lennin", Usuario = "Ocara", Id = 4, Ativo = true, Senha = "SenhaForte" }
        };

        _listaStudent = new List<StudentDTO>
        {
            new StudentDTO { Name = "James", User = "James", Id = 1, Active = true },
            new StudentDTO { Name = "Mary", User = "Mary", Id = 2, Active = true },
            new StudentDTO { Name = "Peter", User = "Spider", Id = 3, Active = true },
            new StudentDTO { Name = "Lennin", User = "Ocara", Id = 4, Active = true }
        };

        _cryptServiceMock = new Mock<ICryptService>();
        _studentRepository = new Mock<IStudentRepository>();
        _mapperMock = new Mock<IMapper>();

        _studentService = new StudentService(_studentRepository.Object, _cryptServiceMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Deve_Retornar_Todos_Os_Alunos()
    {
        //Arrange
        _studentRepository.Setup(x => x.GetAll()).ReturnsAsync(_listaAlunos);
        _mapperMock.Setup(x => x.Map<IEnumerable<StudentDTO>>(_listaAlunos)).Returns(_listaStudent);

        //Act
        var result = await _studentService.GetAll();

        //Assert
        Assert.Equal(4, result.Count());

    }

    [Fact]
    public async Task Deve_Retornar_O_Aluno_Por_Id()
    {
        //Arrange
        _studentRepository.Setup(x => x.GetById(1)).ReturnsAsync(_listaAlunos[0]);
        _mapperMock.Setup(x => x.Map<StudentDTO>(_listaAlunos[0])).Returns(_listaStudent[0]);

        //Act
        var result = await _studentService.GetById(1);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(result.User, _listaStudent[0].User);
    }

    [Fact]
    public async Task Deve_Retornar_O_Aluno_Por_Nome()
    {
        //Arrange
        _studentRepository.Setup(x => x.GetByName("James")).ReturnsAsync(_listaAlunos[0]);
        _mapperMock.Setup(x => x.Map<StudentDTO>(_listaAlunos[0])).Returns(_listaStudent[0]);

        //Act
        var result = await _studentService.GetByName("James");

        //Assert
        Assert.NotNull(result);
        Assert.Equal(result.User, _listaStudent[0].User);
    }

    [Fact]
    public async Task Deve_Retornar_O_Aluno_Por_Usuario()
    {
        //Arrange
        _studentRepository.Setup(x => x.GetByUser("James")).ReturnsAsync(_listaAlunos[0]);
        _mapperMock.Setup(x => x.Map<StudentDTO>(_listaAlunos[0])).Returns(_listaStudent[0]);

        //Act
        var result = await _studentService.GetByUser("James");

        //Assert
        Assert.NotNull(result);
        Assert.Equal(result.User, _listaStudent[0].User);
    }

    [Fact]
    public async Task Deve_Retornar_O_Aluno_Por_Usuario_E_Senha()
    {
        //Arrange
        _studentRepository.Setup(x => x.GetByUserPassword("James", "SenhaForte")).ReturnsAsync(_listaAlunos[0]);
        _mapperMock.Setup(x => x.Map<StudentDTO>(_listaAlunos[0])).Returns(_listaStudent[0]);

        //Act
        var result = await _studentService.GetByUserPassword("James", "SenhaForte");

        //Assert
        Assert.NotNull(result);
        Assert.Equal(result.User, _listaStudent[0].User);
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Ao_Procurar_Aluno_Por_Usuario_E_Senha()
    {
        //Arrange

        //Act
        var result = Assert.ThrowsAsync<ExceptionAPI>(() => _studentService.GetByUserPassword("James", "SenhaForte"));

        //Assert
        Assert.NotNull(result);
        Assert.Equal("Usuario ou senha invalidos.", result.Result.Mensagem);
    }

    [Fact]
    public async Task Deve_Criar_Aluno_Com_Sucesso()
    {
        //Arrange
        var register = new RegisterStudentDTO { User = "James", Name = "James", Password = "Senha@1234", Active = true };

        _studentRepository.Setup(x => x.Create(It.IsAny<Aluno>())).ReturnsAsync(_listaAlunos[0]);
        _mapperMock.Setup(x => x.Map<StudentDTO>(_listaAlunos[0])).Returns(_listaStudent[0]);

        //Act
        var result = await _studentService.Create(register);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(register.User, result.User);
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Ao_Criar_Aluno()
    {
        //Arrange
        var register = new RegisterStudentDTO { User = "James", Name = "James", Password = "Senha@1234", Active = true };

        _studentRepository.Setup(x => x.GetByUser("James")).ReturnsAsync(_listaAlunos[0]);
        

        //Act
        var result = Assert.ThrowsAsync<ExceptionAPI>(() => _studentService.Create(register));

        //Assert
        Assert.NotNull(result);
        Assert.Equal("Nome de usúario não disponivel.", result.Result.Mensagem);
    }

    [Fact]
    public async Task Deve_Atualizar_Aluno_Com_Sucesso()
    {
        //Arrange
        var update = new UpdateStudentDTO { User = "James", Name = "James", Password = "Senha@1234", Active = true, Id = 1 };

        _studentRepository.Setup(x => x.Update(It.IsAny<Aluno>())).ReturnsAsync(_listaAlunos[0]);
        _cryptServiceMock.Setup(x => x.EncryptPassword(It.IsAny<string>())).Returns("SenhaCriptada");
        _mapperMock.Setup(x => x.Map<StudentDTO>(_listaAlunos[0])).Returns(_listaStudent[0]);

        //Act
        var result = await _studentService.Update(update);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(update.User, result.User);
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Ao_Atualizar_Aluno()
    {
        //Arrange
        var update = new UpdateStudentDTO { User = "James", Name = "Joao", Password = "Senha@1234", Active = true, Id = 2 };

        _studentRepository.Setup(x => x.GetByUser("James")).ReturnsAsync(_listaAlunos[0]);

        //Act
        var result = Assert.ThrowsAsync<ExceptionAPI>(() => _studentService.Update(update));

        //Assert
        Assert.NotNull(result);
        Assert.Equal("Nome de usúario não disponivel.", result.Result.Mensagem);
    }

    [Fact]
    public async Task Deve_Deletar_Aluno()
    {
        //Arrange
        _studentRepository.Setup(x => x.Delete(1)).ReturnsAsync(true);

        //Act
        var result = await _studentService.Delete(1);

        //Assert
        Assert.NotNull(result);
        Assert.True(result);
    }
}
