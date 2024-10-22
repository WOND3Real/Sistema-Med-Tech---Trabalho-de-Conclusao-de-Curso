using Microsoft.AspNetCore.Mvc;
using SMT.Models;
using System.Threading.Tasks;
using System;

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
        [HttpPost]
    public async Task<IActionResult> AdicionarPaciente([FromBody] Paciente paciente)
    {
        Console.WriteLine("AdicionarPaciente chamado");

        if (paciente == null)
        {
            Console.WriteLine("Erro: Paciente é nulo");
            return BadRequest("Paciente não pode ser nulo.");
        }

        if (!ModelState.IsValid)
        {
            Console.WriteLine("Erro de validação:");
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return BadRequest(ModelState);
        }

        try
        {
            Console.WriteLine($"Adicionando paciente: {paciente.CpfPaci}");
            var resultado = await _pacienteService.AdicionarPaciente(paciente);

            if (!resultado)
            {
                Console.WriteLine("Erro: Paciente já existe");
                return Conflict("Um paciente com este CPF já existe.");
            }

            return CreatedAtAction(nameof(BuscarPaciente), new { cpf = paciente.CpfPaci }, paciente);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao adicionar paciente: {ex.Message}");
            return StatusCode(500, "Erro interno do servidor. Tente novamente mais tarde.");
        }
    }


        // PUT: api/Paciente
        [HttpPut]
        public async Task<IActionResult> AtualizarPaciente([FromBody] Paciente paciente)
        {
            Console.WriteLine($"AtualizarPaciente chamado para CPF: {paciente.CpfPaci}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Erro de validação:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            try
            {
                var pacienteExistente = await _pacienteService.BuscarPaciente(paciente.CpfPaci);
                if (pacienteExistente == null)
                {
                    Console.WriteLine($"Paciente com CPF {paciente.CpfPaci} não encontrado");
                    return NotFound($"Paciente com CPF {paciente.CpfPaci} não encontrado.");
                }

                await _pacienteService.AtualizarPaciente(paciente);
                Console.WriteLine("Paciente atualizado com sucesso");
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar paciente: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor. Tente novamente mais tarde.");
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
