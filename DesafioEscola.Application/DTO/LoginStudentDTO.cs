using System.ComponentModel.DataAnnotations;

namespace DesafioEscola.Application.DTO;

public class LoginStudentDTO
{
    [Required(ErrorMessage = "Campo obrigatório")]
    public string User { get; set; }

    [Required(ErrorMessage = "Campo obrigatório")]
    public string Password { get; set; }
}
