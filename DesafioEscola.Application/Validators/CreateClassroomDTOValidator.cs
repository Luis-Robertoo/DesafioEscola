using DesafioEscola.Application.DTO;
using DesafioEscola.Crosscutting.Helpers;
using FluentValidation;

namespace DesafioEscola.Application.Validators;

public class CreateClassroomDTOValidator :  AbstractValidator<CreateClassroomDTO>
{
    public CreateClassroomDTOValidator() 
    {
        var yearNow = DateTime.Now.Year;

        RuleFor(x => x.Name)
            .NotNull().WithMessage("O nome da turma não pode ser nula")
            .NotEmpty().WithMessage("O nome da turma não pode estar vazia")
            .MinimumLength(4).WithMessage("O nome da turma deve ter no minimo 4 caracteres");

        RuleFor(x => x.Year)
            .NotNull().WithMessage("O campo Ano não pode ser nulo")
            .NotEmpty().WithMessage("O campo Ano não pode estar vazio")
            .GreaterThanOrEqualTo(yearNow).WithMessage(DefaultMessages.ANO_DA_TURMA_INVALIDO);

        RuleFor(x => x.Course_Id)
            .NotNull().WithMessage("O codigo do curso não pode ser nulo")
            .NotEmpty().WithMessage("O codigo do curso não pode estar vazio");
    }
}
