using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DesafioEscola.Web.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Token { get; set; }
        public void OnGet()
        {
            
        }

        public void OnPost() 
        {
            
        }

        public object Te(object t)
        {
            return new();
        }
    }
}
