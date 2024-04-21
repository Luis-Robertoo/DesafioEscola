using AutoMapper;
using DesafioEscola.Application.DTO;
using DesafioEscola.Domain.Entities;

namespace DesafioEscola.Application.Mapping;

public class Mapping : Profile
{
    public Mapping()
    {
        
        CreateMap<Aluno, StudentDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nome))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Usuario))
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Ativo))
            .ReverseMap()
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.Active));

        CreateMap<Aluno, UpdateStudentDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nome))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Usuario))
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Ativo))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Senha))
            .ReverseMap()
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.Active))
            .ForMember(dest => dest.Senha, opt => opt.MapFrom(src => src.Password));

        CreateMap<Aluno, RegisterStudentDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nome))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Usuario))
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Ativo))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Senha))
            .ReverseMap()
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.Active))
            .ForMember(dest => dest.Senha, opt => opt.MapFrom(src => src.Password));


        CreateMap<Turma, ClassroomDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nome))
            .ForMember(dest => dest.Course_Id, opt => opt.MapFrom(src => src.Curso_Id))
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Ativo))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Ano))
            .ReverseMap()
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Curso_Id, opt => opt.MapFrom(src => src.Course_Id))
            .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.Active))
            .ForMember(dest => dest.Ano, opt => opt.MapFrom(src => src.Year));


        CreateMap<Turma, CreateClassroomDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nome))
            .ForMember(dest => dest.Course_Id, opt => opt.MapFrom(src => src.Curso_Id))
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Ativo))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Ano))
            .ReverseMap()
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Curso_Id, opt => opt.MapFrom(src => src.Course_Id))
            .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.Active))
            .ForMember(dest => dest.Ano, opt => opt.MapFrom(src => src.Year));

        CreateMap<AlunoTurma, StudentClassroomDTO>()
            .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.Aluno_Id))
            .ForMember(dest => dest.ClassroomId, opt => opt.MapFrom(src => src.Turma_Id))
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Ativo))
            .ReverseMap()
            .ForMember(dest => dest.Aluno_Id, opt => opt.MapFrom(src => src.StudentId))
            .ForMember(dest => dest.Turma_Id, opt => opt.MapFrom(src => src.ClassroomId))
            .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.Active));


        CreateMap<AlunoTurma, CreateStudentClassroomDTO>()
            .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.Aluno_Id))
            .ForMember(dest => dest.ClassroomId, opt => opt.MapFrom(src => src.Turma_Id))
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Ativo))
            .ReverseMap()
            .ForMember(dest => dest.Aluno_Id, opt => opt.MapFrom(src => src.StudentId))
            .ForMember(dest => dest.Turma_Id, opt => opt.MapFrom(src => src.ClassroomId))
            .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.Active));
    }
}
