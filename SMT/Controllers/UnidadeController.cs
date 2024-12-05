using Microsoft.AspNetCore.Mvc;
using SMT.Models;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace SMT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnidadeController : ControllerBase
    {
        private readonly UnidadeService _unidadeService;

        public UnidadeController(UnidadeService unidadeService)
        {
            _unidadeService = unidadeService;
        }

        // POST: api/Unidade
        [HttpPost]
        public async Task<IActionResult> AdicionarUnidade([FromBody] JsonElement json)
        {
            try
            {
                // Extrair valores do JSON
                string nomeUnidade = json.GetProperty("Nomeunidade").GetString() ?? throw new ArgumentNullException("Nomeunidade não pode ser nulo.");
                string cepUni = json.GetProperty("Cepuni").GetString() ?? throw new ArgumentNullException("Cepuni não pode ser nulo.");
                string logradouroUni = json.GetProperty("Logradourouni").GetString() ?? throw new ArgumentNullException("Logradourouni não pode ser nulo.");
                string numeroUni = json.GetProperty("Numerouni").GetString() ?? throw new ArgumentNullException("Numerouni não pode ser nulo.");
                string bairroUni = json.GetProperty("Bairrouni").GetString() ?? throw new ArgumentNullException("Bairrouni não pode ser nulo.");
                string cidadeUni = json.GetProperty("Cidadeuni").GetString() ?? throw new ArgumentNullException("Cidadeuni não pode ser nulo.");
                string estadoUni = json.GetProperty("Estadouni").GetString() ?? throw new ArgumentNullException("Estadouni não pode ser nulo.");
                string paisUni = json.GetProperty("Paisuni").GetString() ?? throw new ArgumentNullException("Paisuni não pode ser nulo.");

                // Criar o objeto Unidade
                var unidade = new Unidade
                {
                    Nomeunidade = nomeUnidade,
                    Cepuni = cepUni,
                    Logradourouni = logradouroUni,
                    Numerouni = numeroUni,
                    Bairrouni = bairroUni,
                    Cidadeuni = cidadeUni,
                    Estadouni = estadoUni,
                    Paisuni = paisUni
                };

                // Validar as propriedades do modelo
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(unidade);
                if (!Validator.TryValidateObject(unidade, validationContext, validationResults, true))
                {
                    return BadRequest(validationResults);
                }

                // Inserir a unidade no banco de dados
                var result = await InserirUnidade(unidade);

                if (result)
                {
                    return CreatedAtAction(nameof(BuscarUnidade), new { id = unidade.Idunidade }, unidade);
                }
                else
                {
                    return BadRequest("Erro ao adicionar a unidade.");
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

        private async Task<bool> InserirUnidade(Unidade unidade)
        {
            try
            {
                return await _unidadeService.AdicionarUnidade(unidade);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir unidade: {ex.Message}", ex);
            }
        }

        // PUT: api/Unidade
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarUnidade(int id, [FromBody] JsonElement json)
        {
            try
            {
                // Buscar a unidade existente
                var unidadeExistente = await _unidadeService.BuscarUnidade(id);
                if (unidadeExistente == null)
                {
                    return NotFound($"Unidade com ID {id} não encontrada.");
                }

                // Atualizar somente os campos presentes no JSON
                if (json.TryGetProperty("Nomeunidade", out JsonElement nomeunidadeElement))
                {
                    unidadeExistente.Nomeunidade = nomeunidadeElement.GetString();
                }
                if (json.TryGetProperty("Cepuni", out JsonElement cepuniElement))
                {
                    unidadeExistente.Cepuni = cepuniElement.GetString();
                }
                if (json.TryGetProperty("Logradourouni", out JsonElement logradourouniElement))
                {
                    unidadeExistente.Logradourouni = logradourouniElement.GetString();
                }
                if (json.TryGetProperty("Numerouni", out JsonElement numerouniElement))
                {
                    unidadeExistente.Numerouni = numerouniElement.GetString();
                }
                if (json.TryGetProperty("Bairrouni", out JsonElement bairrouniElement))
                {
                    unidadeExistente.Bairrouni = bairrouniElement.GetString();
                }
                if (json.TryGetProperty("Cidadeuni", out JsonElement cidadeuniElement))
                {
                    unidadeExistente.Cidadeuni = cidadeuniElement.GetString();
                }
                if (json.TryGetProperty("Estadouni", out JsonElement estadouniElement))
                {
                    unidadeExistente.Estadouni = estadouniElement.GetString();
                }
                if (json.TryGetProperty("Paisuni", out JsonElement paisuniElement))
                {
                    unidadeExistente.Paisuni = paisuniElement.GetString();
                }

                // Validar as propriedades do modelo
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(unidadeExistente);
                if (!Validator.TryValidateObject(unidadeExistente, validationContext, validationResults, true))
                {
                    return BadRequest(validationResults);
                }

                // Atualizar a unidade no banco de dados
                var result = await _unidadeService.AtualizarUnidade(unidadeExistente);

                if (result)
                {
                    return NoContent(); // Sucesso: Sem conteúdo para retornar
                }
                else
                {
                    return BadRequest("Erro ao atualizar a unidade.");
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

        // GET: api/Unidade/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarUnidade(int id)
        {
            try
            {
                var unidade = await _unidadeService.BuscarUnidade(id);
                if (unidade == null)
                {
                    return NotFound($"Unidade com ID {id} não encontrada.");
                }
                return Ok(unidade);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar unidade: {ex.Message}");
            }
        }

        // DELETE: api/Unidade/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarUnidade(int id)
        {
            try
            {
                var unidadeExistente = await _unidadeService.BuscarUnidade(id);
                if (unidadeExistente == null)
                {
                    return NotFound($"Unidade com ID {id} não encontrada.");
                }

                await _unidadeService.DeletarUnidade(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar unidade: {ex.Message}");
            }
        }
    }
}
