using Microsoft.AspNetCore.Mvc;
using SMT.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Threading.Tasks;

namespace SMT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultaController : ControllerBase
    {
        private readonly ConsultaService _consultaService;

        public ConsultaController(ConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        // POST: api/Consulta
        [HttpPost]
        public async Task<IActionResult> AdicionarConsulta([FromBody] JsonElement json)
        {
            try
            {
                // Extrair valores do JSON
                string pacienteCpfPaci = json.GetProperty("PacienteCpfPaci").GetString() ?? throw new ArgumentNullException("PacienteCpfPaci não pode ser nulo.");
                string atendenteCtpsAtend = json.GetProperty("AtendenteCtpsAtend").GetString() ?? throw new ArgumentNullException("AtendenteCtpsAtend não pode ser nulo.");
                string medicoCrmMed = json.GetProperty("MedicoCrmMed").GetString() ?? throw new ArgumentNullException("MedicoCrmMed não pode ser nulo.");
                int unidadeIdUnidade = json.GetProperty("UnidadeIdUnidade").GetInt32();
                int especialidadeIdEspecialidade = json.GetProperty("EspecialidadeIdEspecialidade").GetInt32();
                DateTime dataConsul = json.GetProperty("DataConsul").GetDateTime();

                // Converter DateTime para DateOnly
                DateOnly dataConsulOnly = DateOnly.FromDateTime(dataConsul);

                // HoraConsul é representado como string, por exemplo, "14:30:00"
                string horaConsulString = json.GetProperty("HoraConsul").GetString() ?? throw new ArgumentNullException("HoraConsul não pode ser nulo.");
                TimeSpan horaConsulSpan = TimeSpan.Parse(horaConsulString);

                // Converter TimeSpan para TimeOnly
                TimeOnly horaConsul = TimeOnly.FromTimeSpan(horaConsulSpan);

                string statusConsul = json.GetProperty("StatusConsul").GetString() ?? throw new ArgumentNullException("StatusConsul não pode ser nulo.");

                // Criar o objeto Consulta
                var consulta = new Consultum
                {
                    PacienteCpfPaci = pacienteCpfPaci,
                    AtendenteCtpsAtend = atendenteCtpsAtend,
                    MedicoCrmMed = medicoCrmMed,
                    UnidadeIdUnidade = unidadeIdUnidade,
                    EspecialidadeIdEspecialidade = especialidadeIdEspecialidade,
                    DataConsul = dataConsulOnly,  // Usar DateOnly aqui
                    HoraConsul = horaConsul, // Usar TimeOnly aqui
                    StatusConsul = statusConsul
                };

                // Validar as propriedades do modelo
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(consulta);
                if (!Validator.TryValidateObject(consulta, validationContext, validationResults, true))
                {
                    return BadRequest(validationResults);
                }

                // Inserir a consulta no banco de dados
                var result = await InserirConsulta(consulta);

                if (result)
                {
                    return CreatedAtAction(nameof(BuscarConsulta), new { id = consulta.IdConsulta }, consulta);
                }
                else
                {
                    return BadRequest("Erro ao adicionar a consulta.");
                }
            }
            catch (JsonException ex)
            {
                // Erro ao processar o JSON
                return BadRequest($"Erro ao processar o JSON: {ex.Message}");
            }
            catch (FormatException ex)
            {
                // Erro ao converter TimeSpan
                return BadRequest($"Erro ao converter o horário: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Qualquer outro erro
                return StatusCode(500, $"Erro inesperado: {ex.Message}");
            }
        }

        private async Task<bool> InserirConsulta(Consultum consulta)
        {
            try
            {
                return await _consultaService.AdicionarConsulta(consulta);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir consulta: {ex.Message}", ex);
            }
        }

        // PUT: api/Consulta/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarConsulta(int id, [FromBody] JsonElement json)
        {
            try
            {
                // Buscar a consulta existente
                var consultaExistente = await _consultaService.BuscarConsulta(id);
                if (consultaExistente == null)
                {
                    return NotFound($"Consulta com ID {id} não encontrada.");
                }

                // Atualizar somente os campos presentes no JSON
                if (json.TryGetProperty("PacienteCpfPaci", out JsonElement pacienteCpfPaciElement))
                {
                    consultaExistente.PacienteCpfPaci = pacienteCpfPaciElement.GetString();
                }
                if (json.TryGetProperty("AtendenteCtpsAtend", out JsonElement atendenteCtpsAtendElement))
                {
                    consultaExistente.AtendenteCtpsAtend = atendenteCtpsAtendElement.GetString();
                }
                if (json.TryGetProperty("MedicoCrmMed", out JsonElement medicoCrmMedElement))
                {
                    consultaExistente.MedicoCrmMed = medicoCrmMedElement.GetString();
                }
                if (json.TryGetProperty("UnidadeIdUnidade", out JsonElement unidadeIdUnidadeElement))
                {
                    consultaExistente.UnidadeIdUnidade = unidadeIdUnidadeElement.GetInt32();
                }
                if (json.TryGetProperty("EspecialidadeIdEspecialidade", out JsonElement especialidadeIdEspecialidadeElement))
                {
                    consultaExistente.EspecialidadeIdEspecialidade = especialidadeIdEspecialidadeElement.GetInt32();
                }
                if (json.TryGetProperty("DataConsul", out JsonElement dataConsulElement))
                {
                    consultaExistente.DataConsul = DateOnly.FromDateTime(dataConsulElement.GetDateTime());
                }
                if (json.TryGetProperty("HoraConsul", out JsonElement horaConsulElement))
                {
                    consultaExistente.HoraConsul = TimeOnly.FromTimeSpan(TimeSpan.Parse(horaConsulElement.GetString()));
                }
                if (json.TryGetProperty("StatusConsul", out JsonElement statusConsulElement))
                {
                    consultaExistente.StatusConsul = statusConsulElement.GetString();
                }

                // Validar as propriedades do modelo
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(consultaExistente);
                if (!Validator.TryValidateObject(consultaExistente, validationContext, validationResults, true))
                {
                    return BadRequest(validationResults);
                }

                // Atualizar a consulta no banco de dados
                var result = await _consultaService.AtualizarConsulta(consultaExistente);

                if (result)
                {
                    return NoContent(); // Sucesso: Sem conteúdo para retornar
                }
                else
                {
                    return BadRequest("Erro ao atualizar a consulta.");
                }
            }
            catch (JsonException ex)
            {
                // Erro ao processar o JSON
                return BadRequest($"Erro ao processar o JSON: {ex.Message}");
            }
            catch (FormatException ex)
            {
                // Erro ao converter TimeSpan
                return BadRequest($"Erro ao converter o horário: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Qualquer outro erro
                return StatusCode(500, $"Erro inesperado: {ex.Message}");
            }
        }

        // GET: api/Consulta/{id}
        [HttpGet("{id}")]
        public IActionResult BuscarConsulta(int id)
        {
            try
            {
                var consulta = _consultaService.BuscarConsulta(id);
                if (consulta == null)
                {
                    return NotFound($"Consulta com ID {id} não encontrada.");
                }
                return Ok(consulta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar consulta: {ex.Message}");
            }
        }

        // DELETE: api/Consulta/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletarConsulta(int id)
        {
            try
            {
                var consultaExistente = _consultaService.BuscarConsulta(id);
                if (consultaExistente == null)
                {
                    return NotFound($"Consulta com ID {id} não encontrada.");
                }

                _consultaService.DeletarConsulta(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar consulta: {ex.Message}");
            }
        }
    }
}
