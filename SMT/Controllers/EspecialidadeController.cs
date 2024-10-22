using Microsoft.AspNetCore.Mvc;
using SMT.Models;
using System;

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
        public IActionResult AdicionarEspecialidade([FromBody] Especialidade especialidade)
        {
            Console.WriteLine("AdicionarEspecialidade chamado");

            if (especialidade == null)
            {
                Console.WriteLine("Erro: Especialidade é nula");
                return BadRequest("Especialidade não pode ser nula.");
            }

            Console.WriteLine($"Adicionando especialidade: {especialidade.Nomeespec}");
            _especialidadeService.AdicionarEspecialidade(especialidade);
            return CreatedAtAction(nameof(BuscarEspecialidade), new { id = especialidade.Idespecialidade }, especialidade);
        }

        // PUT: api/Especialidade
        [HttpPut]
        public IActionResult AtualizarEspecialidade([FromBody] Especialidade especialidade)
        {
            Console.WriteLine($"AtualizarEspecialidade chamado para ID: {especialidade.Idespecialidade}");
            _especialidadeService.AtualizarEspecialidade(especialidade);
            Console.WriteLine("Especialidade atualizada com sucesso");
            return NoContent();
        }

        // GET: api/Especialidade/{id}
        [HttpGet("{id}")]
        public IActionResult BuscarEspecialidade(int id)
        {
            Console.WriteLine($"BuscarEspecialidade chamado para ID: {id}");
            var especialidade = _especialidadeService.BuscarEspecialidade(id);
            if (especialidade == null)
            {
                Console.WriteLine($"Especialidade com ID {id} não encontrada");
                return NotFound();
            }
            Console.WriteLine($"Especialidade encontrada: {especialidade.Nomeespec}");
            return Ok(especialidade);
        }

        // DELETE: api/Especialidade/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletarEspecialidade(int id)
        {
            Console.WriteLine($"DeletarEspecialidade chamado para ID: {id}");
            _especialidadeService.DeletarEspecialidade(id);
            Console.WriteLine($"Especialidade com ID {id} deletada");
            return NoContent();
        }
    }
}
