using DesafioEscola.Application.DTO;
using DesafioEscola.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioEscola.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginStudentDTO dto)
        { 
            var result = await _authenticationService.Login(dto);
            return Ok(result);
        }

        //[HttpPost]
        //[Route("Cadastrar")]
        //[Authorize(Roles = "ADMIN")]
        //public async Task<IActionResult> Register([FromBody] RegisterStudentDTO dto)
        //{
        //    var result = await _authenticationService.Register(dto);
        //    return Ok(result);
        //}
    }
}
