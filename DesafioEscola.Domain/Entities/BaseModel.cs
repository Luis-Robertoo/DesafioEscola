using System.ComponentModel.DataAnnotations;

namespace DesafioEscola.Domain.Entities;

public class BaseModel
{
    [Key]
    public int Id { get; set; }
}
