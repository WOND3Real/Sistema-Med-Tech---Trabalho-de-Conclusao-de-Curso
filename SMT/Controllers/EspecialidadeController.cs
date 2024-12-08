using Microsoft.AspNetCore.Mvc;
using SMT.Models;
using System;
using System.Threading.Tasks;

namespace SMT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EspecialidadeController : ControllerBase
    {
        private readonly EspecialidadeService _especialidadeService;

        public EspecialidadeController(EspecialidadeService especialidadeService)
        {
            _especialidadeService = especialidadeService;
        }

        // GET: api/Especialidade/{nome}
        [HttpGet("{nome}")]
        public async Task<IActionResult> BuscarIdPorNome(string nome)
        {
            try
            {
                var idEspecialidade = await _especialidadeService.BuscarIdPorNome(nome);

                if (idEspecialidade.HasValue)
                {
                    return Ok(new { IdEspecialidade = idEspecialidade.Value });
                }
                else
                {
                    return NotFound($"Especialidade com nome '{nome}' não encontrada.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar especialidade: {ex.Message}");
            }
        }
    }
}
