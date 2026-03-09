using MicroondasBennerAPI.Service.Contracts;
using MicroondasBennerCommon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MicroondasBennerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProgramasPersonalizadosController(IProgramasPersonalizadosService programasPersonalizadosService) : ControllerBase
    {
        private readonly IProgramasPersonalizadosService _programasPersonalizadosService = programasPersonalizadosService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramaAquecimento>>> GetAll()
        {
            var programas = await _programasPersonalizadosService.GetAllAsync();
            return Ok(programas);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ProgramaAquecimento>> GetById(int id)
        {
            var programa = await _programasPersonalizadosService.GetByIdAsync(id);

            if (programa == null)
                return NotFound();

            return Ok(programa);
        }

        [HttpGet("Usuario/{id}")]
        public async Task<ActionResult<IEnumerable<ProgramaAquecimento>>> GetByUserId(int id)
        {
            var programa = await _programasPersonalizadosService.GetByUserIdAsync(id);

            if (programa == null)
                return NotFound();

            return Ok(programa);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Insert([FromBody] ProgramaAquecimento programa)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            programa.IdUsuario = int.Parse(userId!);
            var id = await _programasPersonalizadosService.InsertAsync(programa);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                programa
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] ProgramaAquecimento programa)
        {
            if (id != programa.Id)
                return BadRequest("Id da URL diferente do objeto");

            var updated = await _programasPersonalizadosService.UpdateAsync(programa);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _programasPersonalizadosService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
