using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMT.Models;
using System.Text.Json;
using System;
using System.Threading.Tasks;

namespace SMT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtendenteController : ControllerBase
    {
        private readonly AtendenteService _atendenteService;

        public AtendenteController(AtendenteService atendenteService)
        {
            _atendenteService = atendenteService;
        }

        // POST: api/Atendente
        [HttpPost]
        public async Task<IActionResult> AdicionarAtendente([FromBody] JsonElement json)
        {
            try
            {
                // Extrair valores do JSON
                string ctpsatend = json.GetProperty("Ctpsatend").GetString() ?? throw new ArgumentNullException("Ctpsatend não pode ser nulo.");
                string nomeatend = json.GetProperty("Nomeatend").GetString() ?? throw new ArgumentNullException("Nomeatend não pode ser nulo.");
                string sobrenomeatend = json.GetProperty("Sobrenomeatend").GetString() ?? throw new ArgumentNullException("Sobrenomeatend não pode ser nulo.");
                string inicioturnoatend = json.GetProperty("Inicioturnoatend").GetString() ?? throw new ArgumentNullException("Inicioturnoatend não pode ser nulo.");
                string fimturnoatend = json.GetProperty("Fimturnoatend").GetString() ?? throw new ArgumentNullException("Fimturnoatend não pode ser nulo.");
                string senhaatend = json.GetProperty("Senhaatend").GetString() ?? throw new ArgumentNullException("Senhaatend não pode ser nulo.");

                // Criar o objeto Atendente
                var atendente = new Atendente
                {
                    Ctpsatend = ctpsatend,
                    Nomeatend = nomeatend,
                    Sobrenomeatend = sobrenomeatend,
                    Inicioturnoatend = TimeOnly.Parse(inicioturnoatend),
                    Fimturnoatend = TimeOnly.Parse(fimturnoatend),
                    Senhaatend = senhaatend
                };

                // Inserir atendente no banco de dados
                _atendenteService.AdicionarAtendente(atendente);
                return CreatedAtAction(nameof(BuscarAtendente), new { ctps = atendente.Ctpsatend }, atendente);
            }
            catch (JsonException ex)
            {
                return BadRequest($"Erro ao processar o JSON: {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Erro ao acessar o banco de dados: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro inesperado: {ex.Message}");
            }
        }

        // PUT: api/Atendente/{ctps}
        [HttpPut("{ctps}")]
        public async Task<IActionResult> AtualizarAtendenteParcial(string ctps, [FromBody] JsonElement json)
        {
            try
            {
                // Buscar atendente existente
                var atendenteExistente = await _atendenteService.BuscarAtendente(ctps);
                if (atendenteExistente == null)
                {
                    return NotFound($"Atendente com CTPS {ctps} não encontrado.");
                }

                // Atualizar somente os campos presentes no JSON
                if (json.TryGetProperty("Nomeatend", out JsonElement nomeatendElement))
                {
                    atendenteExistente.Nomeatend = nomeatendElement.GetString();
                }
                if (json.TryGetProperty("Sobrenomeatend", out JsonElement sobrenomeatendElement))
                {
                    atendenteExistente.Sobrenomeatend = sobrenomeatendElement.GetString();
                }
                if (json.TryGetProperty("Inicioturnoatend", out JsonElement inicioturnoatendElement))
                {
                    atendenteExistente.Inicioturnoatend = TimeOnly.Parse(inicioturnoatendElement.GetString());
                }
                if (json.TryGetProperty("Fimturnoatend", out JsonElement fimturnoatendElement))
                {
                    atendenteExistente.Fimturnoatend = TimeOnly.Parse(fimturnoatendElement.GetString());
                }
                if (json.TryGetProperty("Senhaatend", out JsonElement senhaatendElement))
                {
                    atendenteExistente.Senhaatend = senhaatendElement.GetString();
                }

                // Atualizar o atendente no banco de dados
                var resultado = await _atendenteService.AtualizarAtendente(atendenteExistente);

                if (resultado)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest("Erro ao atualizar o atendente.");
                }
            }
            catch (JsonException ex)
            {
                return BadRequest($"Erro ao processar o JSON: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro inesperado: {ex.Message}");
            }
        }

        // GET: api/Atendente/{ctps}
        [HttpGet("{ctps}")]
        public async Task<IActionResult> BuscarAtendente(string ctps)
        {
            try
            {
                var atendente = await _atendenteService.BuscarAtendente(ctps);
                if (atendente == null)
                {
                    return NotFound();
                }
                return Ok(atendente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro inesperado: {ex.Message}");
            }
        }

        // DELETE: api/Atendente/{ctps}
        [HttpDelete("{ctps}")]
        public async Task<IActionResult> DeletarAtendente(string ctps)
        {
            try
            {
                bool atendenteDeletado = await _atendenteService.DeletarAtendente(ctps);
                if (atendenteDeletado)
                {
                    return NoContent();
                }
                return NotFound($"Atendente com CTPS {ctps} não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro inesperado: {ex.Message}");
            }
        }
    }
}
