using Microsoft.AspNetCore.Mvc;
using SMT.Models;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace SMT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministradorController : ControllerBase
    {
        private readonly AdministradorService _administradorService;

        public AdministradorController(AdministradorService administradorService)
        {
            _administradorService = administradorService;
        }

        // POST: api/Administrador
        [HttpPost]
        public async Task<IActionResult> AdicionarAdministrador([FromBody] JsonElement json)
        {
            try
            {
                // Extrair valores do JSON
                string nomeAdm = json.GetProperty("NomeAdm").GetString() ?? throw new ArgumentNullException("NomeAdm não pode ser nulo.");
                string sobrenomeAdm = json.GetProperty("SobrenomeAdm").GetString() ?? throw new ArgumentNullException("SobrenomeAdm não pode ser nulo.");
                string senhaAdm = json.GetProperty("SenhaAdm").GetString() ?? throw new ArgumentNullException("SenhaAdm não pode ser nulo.");
                int unidadeIdUnidade = json.GetProperty("UnidadeIdUnidade").GetInt32();

                // Criar o objeto Administrador
                var administrador = new Administrador
                {
                    NomeAdm = nomeAdm,
                    SobrenomeAdm = sobrenomeAdm,
                    SenhaAdm = senhaAdm,
                    UnidadeIdUnidade = unidadeIdUnidade
                };

                // Validar as propriedades do modelo
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(administrador);
                if (!Validator.TryValidateObject(administrador, validationContext, validationResults, true))
                {
                    return BadRequest(validationResults);
                }

                // Inserir o administrador no banco de dados
                var result = await _administradorService.AdicionarAdministrador(administrador);

                if (result)
                {
                    return CreatedAtAction(nameof(BuscarAdministrador), new { id = administrador.IdAdministrador }, administrador);
                }
                else
                {
                    return BadRequest("Erro ao adicionar o administrador.");
                }
            }
            catch (JsonException ex)
            {
                // Erro ao processar o JSON
                return BadRequest($"Erro ao processar o JSON: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Qualquer outro erro
                return StatusCode(500, $"Erro inesperado: {ex.Message}");
            }
        }

        // PUT: api/Administrador/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarAdministrador(int id, [FromBody] JsonElement json)
        {
            try
            {
                // Buscar o administrador existente
                var administradorExistente = await _administradorService.BuscarAdministrador(id);
                if (administradorExistente == null)
                {
                    return NotFound($"Administrador com ID {id} não encontrado.");
                }

                // Atualizar somente os campos presentes no JSON
                if (json.TryGetProperty("NomeAdm", out JsonElement nomeAdmElement))
                {
                    administradorExistente.NomeAdm = nomeAdmElement.GetString();
                }
                if (json.TryGetProperty("SobrenomeAdm", out JsonElement sobrenomeAdmElement))
                {
                    administradorExistente.SobrenomeAdm = sobrenomeAdmElement.GetString();
                }
                if (json.TryGetProperty("SenhaAdm", out JsonElement senhaAdmElement))
                {
                    administradorExistente.SenhaAdm = senhaAdmElement.GetString();
                }
                if (json.TryGetProperty("UnidadeIdUnidade", out JsonElement unidadeIdElement))
                {
                    administradorExistente.UnidadeIdUnidade = unidadeIdElement.GetInt32();
                }

                // Validar as propriedades do modelo
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(administradorExistente);
                if (!Validator.TryValidateObject(administradorExistente, validationContext, validationResults, true))
                {
                    return BadRequest(validationResults);
                }

                // Atualizar o administrador no banco de dados
                var result = await _administradorService.AtualizarAdministrador(administradorExistente);

                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest("Erro ao atualizar o administrador.");
                }
            }
            catch (JsonException ex)
            {
                // Erro ao processar o JSON
                return BadRequest($"Erro ao processar o JSON: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Qualquer outro erro
                return StatusCode(500, $"Erro inesperado: {ex.Message}");
            }
        }

        // GET: api/Administrador/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarAdministrador(int id)
        {
            try
            {
                var administrador = await _administradorService.BuscarAdministrador(id);
                if (administrador == null)
                {
                    return NotFound($"Administrador com ID {id} não encontrado.");
                }
                return Ok(administrador);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar administrador: {ex.Message}");
            }
        }

        // DELETE: api/Administrador/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarAdministrador(int id)
        {
            try
            {
                var administradorExistente = await _administradorService.BuscarAdministrador(id);
                if (administradorExistente == null)
                {
                    return NotFound($"Administrador com ID {id} não encontrado.");
                }

                var result = await _administradorService.DeletarAdministrador(id);
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest("Erro ao deletar o administrador.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar administrador: {ex.Message}");
            }
        }
    }
}
