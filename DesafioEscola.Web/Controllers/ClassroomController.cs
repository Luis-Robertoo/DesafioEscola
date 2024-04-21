using DesafioEscola.Application.DTO;
using DesafioEscola.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioEscola.Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassroomService _classroomService;
        private readonly IStudentClassRoomService _studentClassRoomService;

        public ClassroomController(IClassroomService classroomService, IStudentClassRoomService studentClassRoomService)
        {
            _classroomService = classroomService;
            _studentClassRoomService = studentClassRoomService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? id, [FromQuery] string? name)
        {
            if(id != null)
            {
                var classroom = await _classroomService.GetById((int)id);
                return Ok(classroom);
            }

            if(name != null)
            {
                var classroom = await _classroomService.GetByName(name);
                return Ok(classroom);
            }

            return Ok(await _classroomService.GetAll());
        }
        
        [HttpGet]
        [Route("{id}/Student")]
        public async Task<IActionResult> Get(int id)
        {
            var students = await _studentClassRoomService.GetStudentsByClassroomId(id);
            return Ok(students);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClassroomDTO dto) 
        { 
            var result = await _classroomService.Create(dto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ClassroomDTO dto)
        {
            var result = await _classroomService.Update(dto);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            await _classroomService.Delete(id);
            return Ok();
        }
    }
}
