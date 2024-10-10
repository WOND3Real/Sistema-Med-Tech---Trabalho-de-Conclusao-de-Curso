using Microsoft.AspNetCore.Mvc;
using SMT.Models;

namespace SMT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly PacienteService _pacienteService;

        public PacienteController(PacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        // POST: api/Paciente
        [HttpPost]
        public IActionResult AdicionarPaciente([FromBody] Paciente paciente)
        {
            _pacienteService.AdicionarPaciente(paciente);
            return CreatedAtAction(nameof(BuscarPaciente), new { cpf = paciente.Cpfpaci }, paciente);
        }

        // PUT: api/Paciente
        [HttpPut]
        public IActionResult AtualizarPaciente([FromBody] Paciente paciente)
        {
            _pacienteService.AtualizarPaciente(paciente);
            return NoContent();
        }

        // GET: api/Paciente/{cpf}
        [HttpGet("{cpf}")]
        public IActionResult BuscarPaciente(string cpf)
        {
            var paciente = _pacienteService.BuscarPaciente(cpf);
            if (paciente == null)
            {
                return NotFound();
            }
            return Ok(paciente);
        }

        // DELETE: api/Paciente/{cpf}
        [HttpDelete("{cpf}")]
        public IActionResult DeletarPaciente(string cpf)
        {
            _pacienteService.DeletarPaciente(cpf);
            return NoContent();
        }
    }
}
