using DesafioEscola.Application.DTO;
using DesafioEscola.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DesafioEscola.Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAuthenticationService _authenticationService;
        public string errorUser { get; set; } = string.Empty;
        public string errorPassword { get; set; } = string.Empty;
        public string erroLogin { get; set; } = string.Empty;

        public LoginModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [BindProperty]
        public LoginStudentDTO Login { get; set; }

        public async Task OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                var errosUser = ModelState.FindKeysWithPrefix("Login.User").FirstOrDefault().Value.Errors;
                var errosPass = ModelState.FindKeysWithPrefix("Login.Password").FirstOrDefault().Value.Errors;

                if (errosUser.Count > 0)
                    errorUser = $"*{errosUser.FirstOrDefault().ErrorMessage}";

                if (errosPass.Count > 0)
                    errorPassword = $"*{errosPass.FirstOrDefault().ErrorMessage}";

                return Page();
            }
            try
            {
                var token = await _authenticationService.Login(Login);
            }
            catch(Exception ex)
            {
                erroLogin = ex.Message;
            }

            return Page();
        }
    }
}
