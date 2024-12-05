using Microsoft.AspNetCore.Mvc;
using SMT.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
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

        // POST: api/Especialidade
        [HttpPost]
        public async Task<IActionResult> AdicionarEspecialidade([FromBody] JsonElement json)
        {
            try
            {
                // Extrair valores do JSON
                string nomeEspecialidade = json.GetProperty("Nomeespec").GetString() ?? throw new ArgumentNullException("Nomeespec não pode ser nulo.");

                var especialidadeExistente = await _especialidadeService.BuscarEspecialidade(nomeEspecialidade);
                if (especialidadeExistente != null)
                {
                    return Ok($"Especialidade: {nomeEspecialidade} Já existe.");
                }else{
                    // Criar o objeto Especialidade
                    var especialidade = new Especialidade
                    {
                        Nomeespec = nomeEspecialidade
                    };

                    // Validar as propriedades do modelo
                    var validationResults = new List<ValidationResult>();
                    var validationContext = new ValidationContext(especialidade);
                    if (!Validator.TryValidateObject(especialidade, validationContext, validationResults, true))
                    {
                        return BadRequest(validationResults);
                    }

                    // Inserir a especialidade no banco de dados
                    var result = await InserirEspecialidade(especialidade);

                    if (result)
                    {
                        return CreatedAtAction(nameof(BuscarEspecialidade), new { id = especialidade.Idespecialidade }, especialidade);
                    }
                    else
                    {
                        return BadRequest("Erro ao adicionar a especialidade.");
                    }
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

        private async Task<bool> InserirEspecialidade(Especialidade especialidade)
        {
            try
            {
                return await _especialidadeService.AdicionarEspecialidade(especialidade);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir especialidade: {ex.Message}", ex);
            }
        }

        // PUT: api/Especialidade/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarEspecialidade(int id, [FromBody] JsonElement json)
        {
        try
        {
            // Buscar a especialidade existente
            var especialidadeExistente = await _especialidadeService.BuscarEspecialidade(id);
            if (especialidadeExistente == null)
            {
                return NotFound($"Especialidade com ID {id} não encontrada.");
            }

            // Verificar se o campo "Nomeespec" está presente no JSON
            if (json.TryGetProperty("Nomeespec", out JsonElement nomeespecElement))
            {
                especialidadeExistente.Nomeespec = nomeespecElement.GetString();

                var especialidadeExistenteVerify = await _especialidadeService.BuscarEspecialidade(especialidadeExistente.Nomeespec);
                if (especialidadeExistenteVerify != null)
                {
                    return Ok($"Especialidade: {especialidadeExistente.Nomeespec} já existe.");
                }
                else
                {
                    // Validar as propriedades do modelo
                    var validationResults = new List<ValidationResult>();
                    var validationContext = new ValidationContext(especialidadeExistente);
                    if (!Validator.TryValidateObject(especialidadeExistente, validationContext, validationResults, true))
                    {
                        return BadRequest(validationResults);
                    }

                    // Atualizar a especialidade no banco de dados
                    var result = await _especialidadeService.AtualizarEspecialidade(especialidadeExistente);

                    if (result)
                    {
                        return NoContent(); // Sucesso: Sem conteúdo para retornar
                    }
                    else
                    {
                        return BadRequest("Erro ao atualizar a especialidade.");
                    }
                }
            }

            // Caso o JSON não contenha o campo "Nomeespec", retornar erro
            return BadRequest("Campo 'Nomeespec' não encontrado no JSON.");
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


        // GET: api/Especialidade/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarEspecialidade(int id)
        {
            try
            {
                var especialidade = await _especialidadeService.BuscarEspecialidade(id);
                if (especialidade == null)
                {
                    return NotFound($"Especialidade com ID {id} não encontrada.");
                }
                return Ok(especialidade);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar especialidade: {ex.Message}");
            }
        }

        // DELETE: api/Especialidade/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarEspecialidade(int id)
        {
            try
            {
                var especialidadeExistente = await _especialidadeService.BuscarEspecialidade(id);
                if (especialidadeExistente == null)
                {
                    return NotFound($"Especialidade com ID {id} não encontrada.");
                }

                await _especialidadeService.DeletarEspecialidade(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar especialidade: {ex.Message}");
            }
        }
    }
}