using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DesafioEscola.Web.Pages;

[Authorize]
public class RegistrarModel : PageModel
{
    public void OnGet()
    {

    }
}
