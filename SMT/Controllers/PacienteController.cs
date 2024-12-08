using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMT.Models;
using System.Threading.Tasks;
using System;
using System.Text.Json;
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
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            // Usar o pacienteService para buscar o paciente pelo email e senha
            var paciente = await _pacienteService.BuscarPacientePorCredenciais(loginRequest.EmailPaci, loginRequest.Senhapaci);

            if (paciente == null)
            {
                return Unauthorized(new { message = "Usuário ou senha inválidos!" });
            }

            // Retorna os dados do paciente, incluindo o CPF
            var loginResponse = new LoginResponse
            {
                CpfPaciente = paciente.CpfPaci
            };

            return Ok(loginResponse);
        }




        private async Task<bool> InserirPaciente(Paciente paciente)
        {
            try
            {
                // Usar o método do PacienteService para adicionar o paciente
                return await _pacienteService.AdicionarPaciente(paciente);
            }
            catch (DbUpdateException ex)
            {
                // Lida com erros de atualização no banco de dados
                throw new Exception($"Erro ao inserir paciente: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Lida com qualquer outro erro
                throw new Exception($"Erro inesperado ao inserir paciente: {ex.Message}", ex);
            }
        }



        // PUT: api/Paciente
        [HttpPut("{cpf}")]
        public async Task<IActionResult> AtualizarPacienteParcial(string cpf, [FromBody] JsonElement json)
        {
            try
            {
                // Buscar paciente existente
                var pacienteExistente = await _pacienteService.BuscarPaciente(cpf);
                if (pacienteExistente == null)
                {
                    return NotFound($"Paciente com CPF {cpf} não encontrado.");
                }

                // Atualizar somente os campos presentes no JSON
                if (json.TryGetProperty("NomePaci", out JsonElement nomePaciElement))
                {
                    pacienteExistente.NomePaci = nomePaciElement.GetString();
                }
                if (json.TryGetProperty("SobrenomePaci", out JsonElement sobrenomePaciElement))
                {
                    pacienteExistente.SobrenomePaci = sobrenomePaciElement.GetString();
                }
                if (json.TryGetProperty("Nascimentopaci", out JsonElement nascimentoPaciElement) && nascimentoPaciElement.ValueKind != JsonValueKind.Null)
                {
                    pacienteExistente.NascimentoPaci = DateOnly.FromDateTime(nascimentoPaciElement.GetDateTime().ToUniversalTime());
                }
                if (json.TryGetProperty("GeneroPaci", out JsonElement generoPaciElement))
                {
                    pacienteExistente.GeneroPaci = generoPaciElement.GetString();
                }
                if (json.TryGetProperty("EmailPaci", out JsonElement emailPaciElement))
                {
                    pacienteExistente.EmailPaci = emailPaciElement.GetString();
                }
                if (json.TryGetProperty("TelefonePaci", out JsonElement telefonePaciElement))
                {
                    pacienteExistente.TelefonePaci = telefonePaciElement.GetString();
                }
                if (json.TryGetProperty("Senhapaci", out JsonElement senhaPaciElement))
                {
                    pacienteExistente.SenhaPaci = senhaPaciElement.GetString();
                }

                // Atualizar o paciente no banco de dados com os novos valores
                var resultado = await _pacienteService.AtualizarPaciente(pacienteExistente);

                if (resultado)
                {
                    return NoContent(); // Sucesso: Sem conteúdo para retornar
                }
                else
                {
                    return BadRequest("Erro ao atualizar o paciente.");
                }
            }
            catch (JsonException ex)
            {
                // Erro ao processar o JSON
                return BadRequest($"Erro ao processar o JSON: {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                // Erro ao atualizar o banco de dados
                return StatusCode(500, $"Erro ao acessar o banco de dados: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Qualquer outro erro
                return StatusCode(500, $"Erro inesperado: {ex.Message}");
            }
        }



        // GET: api/Paciente/{cpf}
        [HttpGet("{cpf}")]
        public async Task<IActionResult> BuscarPaciente(string cpf)
        {
            Console.WriteLine($"BuscarPaciente chamado para CPF: {cpf}");
            try
            {
                var paciente = await _pacienteService.BuscarPaciente(cpf);
                if (paciente == null)
                {
                    Console.WriteLine($"Paciente com CPF {cpf} não encontrado");
                    return NotFound();
                }
                Console.WriteLine($"Paciente encontrado: {paciente.CpfPaci}");
                return Ok(paciente);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar paciente: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor. Tente novamente mais tarde.");
            }
        }



        // DELETE: api/Paciente/{cpf}
        [HttpDelete("{cpf}")]
        public async Task<IActionResult> DeletarPaciente(string cpf)
        {
            Console.WriteLine($"DeletarPaciente chamado para CPF: {cpf}");
            try
            {
                var pacienteExistente = await _pacienteService.BuscarPaciente(cpf);
                if (pacienteExistente == null)
                {
                    Console.WriteLine($"Paciente com CPF {cpf} não encontrado");
                    return NotFound($"Paciente com CPF {cpf} não encontrado.");
                }

                await _pacienteService.DeletarPaciente(cpf);
                Console.WriteLine($"Paciente com CPF {cpf} deletado");
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao deletar paciente: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor. Tente novamente mais tarde.");
            }
        }
    }
}