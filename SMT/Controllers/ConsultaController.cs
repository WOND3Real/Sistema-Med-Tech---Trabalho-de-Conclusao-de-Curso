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
                // Extrair valores do JSON de forma segura usando TryGetProperty
                if (!json.TryGetProperty("PacienteCpfpaci", out JsonElement cpfPacienteElement))
                {
                    return BadRequest("PacienteCpfpaci não encontrado no JSON.");
                }
                string cpfPaciente = cpfPacienteElement.GetString();

                if (!json.TryGetProperty("AtendenteCtpsatend", out JsonElement ctpsAtendenteElement))
                {
                    return BadRequest("AtendenteCtpsatend não encontrado no JSON.");
                }
                string ctpsAtendente = ctpsAtendenteElement.GetString();

                if (!json.TryGetProperty("MedicoCrmmed", out JsonElement crmMedicoElement))
                {
                    return BadRequest("MedicoCrmmed não encontrado no JSON.");
                }
                string crmMedico = crmMedicoElement.GetString();

                if (!json.TryGetProperty("UnidadeIdunidade", out JsonElement idUnidadeElement))
                {
                    return BadRequest("UnidadeIdunidade não encontrado no JSON.");
                }
                int idUnidade = idUnidadeElement.GetInt32();

                if (!json.TryGetProperty("EspecialidadeIdespecialidade", out JsonElement idEspecialidadeElement))
                {
                    return BadRequest("EspecialidadeIdespecialidade não encontrado no JSON.");
                }
                int idEspecialidade = idEspecialidadeElement.GetInt32();

                if (!json.TryGetProperty("Dataconsul", out JsonElement dataConsultaElement))
                {
                    return BadRequest("Dataconsul não encontrado no JSON.");
                }
                DateTime dataConsulta = dataConsultaElement.GetDateTime().ToUniversalTime(); // Convertendo para UTC

                if (!json.TryGetProperty("Horaconsul", out JsonElement horaConsultaElement))
                {
                    return BadRequest("Horaconsul não encontrado no JSON.");
                }
                string horaConsultaStr = horaConsultaElement.GetString();
                TimeSpan horaConsulta = TimeSpan.Parse(horaConsultaStr);

                if (!json.TryGetProperty("Statusconsul", out JsonElement statusConsultaElement))
                {
                    return BadRequest("Statusconsul não encontrado no JSON.");
                }
                string statusConsulta = statusConsultaElement.GetString();

                // Criar o objeto Consulta
                var consulta = new Consulta
                {
                    PacienteCpfPaci = cpfPaciente,
                    AtendenteCtpsatend = ctpsAtendente,
                    MedicoCrmmed = crmMedico,
                    UnidadeIdunidade = idUnidade,
                    EspecialidadeIdespecialidade = idEspecialidade,
                    DataConsul = dataConsulta,
                    HoraConsul = horaConsulta,
                    StatusConsul = statusConsulta
                };

                // Inserir consulta no banco de dados
                bool resultado = await _consultaService.AdicionarConsultaAsync(consulta);

                if (resultado)
                {
                    return CreatedAtAction(nameof(BuscarConsulta), new { id = consulta.IdConsulta }, consulta);
                }
                else
                {
                    return StatusCode(500, "Erro ao salvar a consulta.");
                }
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



        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarConsulta(int id)
        {
            var consulta = await _consultaService.BuscarConsultaPorIdAsync(id);
            if (consulta == null)
            {
                return NotFound($"Consulta com ID {id} não encontrada.");
            }

            return Ok(consulta);
        }
    }
}