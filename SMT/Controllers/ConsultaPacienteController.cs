using Microsoft.AspNetCore.Mvc;
using SMT.Models;
using System.Threading.Tasks;

namespace SMT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultaPacienteController : ControllerBase
    {
        private readonly ConsultaPacienteService _consultaPacienteService;

        public ConsultaPacienteController(ConsultaPacienteService consultaPacienteService)
        {
            _consultaPacienteService = consultaPacienteService;
        }



        [HttpGet("{idConsulta}")]
        public async Task<IActionResult> BuscarConsulta(int idConsulta)
        {
            try
            {
                var consulta = await _consultaPacienteService.BuscarConsulta(idConsulta);
                if (consulta == null)
                {
                    return NotFound(new { mensagem = "Consulta não encontrada." });
                }

                return Ok(consulta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno no servidor.", erro = ex.Message });
            }
        }
    }
}
