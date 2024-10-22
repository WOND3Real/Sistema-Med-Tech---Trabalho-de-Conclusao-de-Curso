using Microsoft.AspNetCore.Mvc;
using SMT.Models;
using System;

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
        public IActionResult AdicionarUnidade([FromBody] Unidade unidade)
        {
            Console.WriteLine("AdicionarUnidade chamado");

            if (unidade == null)
            {
                Console.WriteLine("Erro: Unidade é nula");
                return BadRequest("Unidade não pode ser nula.");
            }

            Console.WriteLine($"Adicionando unidade: {unidade.Nomeunidade}");
            _unidadeService.AdicionarUnidade(unidade);
            return CreatedAtAction(nameof(BuscarUnidade), new { id = unidade.Idunidade }, unidade);
        }

        // PUT: api/Unidade
        [HttpPut]
        public IActionResult AtualizarUnidade([FromBody] Unidade unidade)
        {
            Console.WriteLine($"AtualizarUnidade chamado para ID: {unidade.Idunidade}");
            _unidadeService.AtualizarUnidade(unidade);
            Console.WriteLine("Unidade atualizada com sucesso");
            return NoContent();
        }

        // GET: api/Unidade/{id}
        [HttpGet("{id}")]
        public IActionResult BuscarUnidade(int id)
        {
            Console.WriteLine($"BuscarUnidade chamado para ID: {id}");
            var unidade = _unidadeService.BuscarUnidade(id);
            if (unidade == null)
            {
                Console.WriteLine($"Unidade com ID {id} não encontrada");
                return NotFound();
            }
            Console.WriteLine($"Unidade encontrada: {unidade.Nomeunidade}");
            return Ok(unidade);
        }

        // DELETE: api/Unidade/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletarUnidade(int id)
        {
            Console.WriteLine($"DeletarUnidade chamado para ID: {id}");
            _unidadeService.DeletarUnidade(id);
            Console.WriteLine($"Unidade com ID {id} deletada");
            return NoContent();
        }
    }
}
