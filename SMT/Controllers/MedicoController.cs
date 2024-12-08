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
    public class MedicoController : ControllerBase
    {
        private readonly MedicoService _medicoService;

        public MedicoController(MedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        // POST: api/Medico
        [HttpPost]
        public async Task<IActionResult> AdicionarMedico([FromBody] JsonElement json)
        {
            try
            {
                // Extrair valores do JSON
                string crmMed = json.GetProperty("Crmmed").GetString() ?? throw new ArgumentNullException("Crmmed não pode ser nulo.");
                string nomeMed = json.GetProperty("Nomemed").GetString() ?? throw new ArgumentNullException("Nomemed não pode ser nulo.");
                string sobrenomeMed = json.GetProperty("Sobrenomemed").GetString() ?? throw new ArgumentNullException("Sobrenomemed não pode ser nulo.");
                string telefoneMed = json.GetProperty("Telefonemed").GetString() ?? throw new ArgumentNullException("Telefonemed não pode ser nulo.");
                string emailMed = json.GetProperty("Emailmed").GetString() ?? throw new ArgumentNullException("Emailmed não pode ser nulo.");
                string senhaMed = json.GetProperty("Senhamed").GetString() ?? throw new ArgumentNullException("Senhamed não pode ser nulo.");

                // Criar o objeto Medico
                var medico = new Medico
                {
                    Crmmed = crmMed,
                    Nomemed = nomeMed,
                    Sobrenomemed = sobrenomeMed,
                    Telefonemed = telefoneMed,
                    Emailmed = emailMed,
                    Senhamed = senhaMed
                };

                // Inserir médico no banco de dados
                _medicoService.AdicionarMedico(medico); // Garantir a execução assíncrona
                return CreatedAtAction(nameof(BuscarMedico), new { crm = medico.Crmmed }, medico);
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

        // PUT: api/Medico/{crm}
        [HttpPut("{crm}")]
        public async Task<IActionResult> AtualizarMedicoParcial(string crm, [FromBody] JsonElement json)
        {
            try
            {
                // Buscar médico existente
                var medicoExistente = await _medicoService.BuscarMedico(crm); // Esperar a tarefa ser concluída
                if (medicoExistente == null)
                {
                    return NotFound($"Médico com CRM {crm} não encontrado.");
                }

                // Atualizar somente os campos presentes no JSON
                if (json.TryGetProperty("Nomemed", out JsonElement nomemedElement))
                {
                    medicoExistente.Nomemed = nomemedElement.GetString();
                }
                if (json.TryGetProperty("Sobrenomemed", out JsonElement sobrenomemedElement))
                {
                    medicoExistente.Sobrenomemed = sobrenomemedElement.GetString();
                }
                if (json.TryGetProperty("Telefonemed", out JsonElement telefonemedElement))
                {
                    medicoExistente.Telefonemed = telefonemedElement.GetString();
                }
                if (json.TryGetProperty("Emailmed", out JsonElement emailmedElement))
                {
                    medicoExistente.Emailmed = emailmedElement.GetString();
                }
                if (json.TryGetProperty("Senhamed", out JsonElement senhamedElement))
                {
                    medicoExistente.Senhamed = senhamedElement.GetString();
                }

                // Atualizar o médico no banco de dados com os novos valores
                var resultado = await _medicoService.AtualizarMedico(medicoExistente);

                if (resultado)
                {
                    return NoContent(); // Sucesso: Sem conteúdo para retornar
                }
                else
                {
                    return BadRequest("Erro ao atualizar o médico.");
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


        // GET: api/Medico/{crm}
        [HttpGet("{crm}")]
        public async Task<IActionResult> BuscarMedico(string crm)
        {
            try
            {
                var medico = await _medicoService.BuscarMedico(crm);
                if (medico == null)
                {
                    return NotFound();
                }
                return Ok(medico);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro inesperado: {ex.Message}");
            }
        }

        // DELETE: api/Medico/{crm}
        [HttpDelete("{crm}")]
        public async Task<IActionResult> DeletarMedico(string crm)
        {
            try
            {
                bool medicoDeletado = await _medicoService.DeletarMedico(crm);
                if (medicoDeletado)
                {
                    return NoContent(); // Retorna 204 se o médico for deletado
                }
                return NotFound($"Médico com CRM {crm} não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro inesperado: {ex.Message}");
            }
        }
    }
}
