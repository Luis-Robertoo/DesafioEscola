using DesafioEscola.Application.DTO;
using FluentValidation;

namespace DesafioEscola.Application.Validators;

public class UpdateStudentDTOValidator :  AbstractValidator<UpdateStudentDTO>
{
    public UpdateStudentDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("O nome não pode ser nulo")
            .NotEmpty().WithMessage("O nome não pode estar vazio")
            .MinimumLength(4).WithMessage("O nome deve ter no minimo 4 caracteres");

        RuleFor(x => x.User)
            .NotNull().WithMessage("O campo usuario não pode ser nulo")
            .NotEmpty().WithMessage("O campo usuario não pode estar vazio")
            .MinimumLength(4).WithMessage("O campo usuario deve ter no minimo 4 caracteres");

        RuleFor(x => x.Password)
            .NotNull().WithMessage("A senha não pode ser nula")
            .NotEmpty().WithMessage("A senha não pode estar vazia")
            .MinimumLength(8).WithMessage("A senha deve ter no minimo 8 caracteres")
            .MaximumLength(10).WithMessage("A senha deve ter no maximo 10 caracteres")
            .Matches("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[$*&@#])[0-9a-zA-Z$*&@#]{8,}")
                .WithMessage("A senha deve conter ao menos 1 letra maiuscula, 1 letra minuscula, 1 numero e um dos seguintes caracteres especiais '$*&@#'");

        RuleFor(x => x.Id)
            .NotNull().WithMessage("O Id do aluno não pode ser nulo")
            .NotEmpty().WithMessage("O Id do aluno não pode estar vazio");
    }
}
