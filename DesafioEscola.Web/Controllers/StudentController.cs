using DesafioEscola.Application.DTO;
using DesafioEscola.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioEscola.Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class StudentController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IStudentService _studentService;

        public StudentController(IAuthenticationService authenticationService, IStudentService studentService)
        {
            _authenticationService = authenticationService;
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? id, [FromQuery] string? User, [FromQuery] string? Name)
        {
            if(id != null)
            {
                var student = await _studentService.GetById((int)id);
                return Ok(student);
            }  

            if (User != null)
            {
                var student = await _studentService.GetByUser(User);
                return Ok(student);
            }
                
            if (Name != null)
            {
                var student = await _studentService.GetByName(Name);
                return Ok(student);
            }
                
            return Ok(await _studentService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegisterStudentDTO dto) 
        {
            var stundentCreate = await _authenticationService.Register(dto);
            return Ok(stundentCreate);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateStudentDTO dto)
        {
            var student = await _studentService.Update(dto);
            return Ok(student);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            await _studentService.Delete(id);
            return Ok();
        }
    }
}
