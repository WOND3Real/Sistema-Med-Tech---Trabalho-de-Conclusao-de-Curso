using Microsoft.AspNetCore.Mvc;
using SMT.Models;
using System;

namespace SMT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtendenteController : ControllerBase
    {
        private readonly AtendenteService _atendenteService;

        public AtendenteController(AtendenteService atendenteService)
        {
            _atendenteService = atendenteService;
        }

        // POST: api/Atendente
        [HttpPost]
        public IActionResult AdicionarAtendente([FromBody] Atendente atendente)
        {
            Console.WriteLine("AdicionarAtendente chamado");

            if (atendente == null)
            {
                Console.WriteLine("Erro: Atendente é nulo");
                return BadRequest("Atendente não pode ser nulo.");
            }

            Console.WriteLine($"Adicionando atendente: {atendente.Ctpsatend}");
            _atendenteService.AdicionarAtendente(atendente);
            return CreatedAtAction(nameof(BuscarAtendente), new { ctps = atendente.Ctpsatend }, atendente);
        }

        // PUT: api/Atendente
        [HttpPut]
        public IActionResult AtualizarAtendente([FromBody] Atendente atendente)
        {
            Console.WriteLine($"AtualizarAtendente chamado para CTPS: {atendente.Ctpsatend}");
            _atendenteService.AtualizarAtendente(atendente);
            Console.WriteLine("Atendente atualizado com sucesso");
            return NoContent();
        }

        // GET: api/Atendente/{ctps}
        [HttpGet("{ctps}")]
        public IActionResult BuscarAtendente(string ctps)
        {
            Console.WriteLine($"BuscarAtendente chamado para CTPS: {ctps}");
            var atendente = _atendenteService.BuscarAtendente(ctps);
            if (atendente == null)
            {
                Console.WriteLine($"Atendente com CTPS {ctps} não encontrado");
                return NotFound();
            }
            Console.WriteLine($"Atendente encontrado: {atendente.Ctpsatend}");
            return Ok(atendente);
        }

        // DELETE: api/Atendente/{ctps}
        [HttpDelete("{ctps}")]
        public IActionResult DeletarAtendente(string ctps)
        {
            Console.WriteLine($"DeletarAtendente chamado para CTPS: {ctps}");
            _atendenteService.DeletarAtendente(ctps);
            Console.WriteLine($"Atendente com CTPS {ctps} deletado");
            return NoContent();
        }
    }
}
