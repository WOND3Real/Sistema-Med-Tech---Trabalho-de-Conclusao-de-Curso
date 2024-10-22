using Microsoft.AspNetCore.Mvc;
using SMT.Models;
using System;

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
        public IActionResult AdicionarConsulta([FromBody] Consultum consulta)
        {
            Console.WriteLine("AdicionarConsulta chamado");

            if (consulta == null)
            {
                Console.WriteLine("Erro: Consulta é nula");
                return BadRequest("Consulta não pode ser nula.");
            }

            Console.WriteLine($"Adicionando consulta: {consulta.Idconsulta}");
            _consultaService.AdicionarConsulta(consulta);
            return CreatedAtAction(nameof(BuscarConsulta), new { id = consulta.Idconsulta }, consulta);
        }

        // PUT: api/Consulta
        [HttpPut]
        public IActionResult AtualizarConsulta([FromBody] Consultum consulta)
        {
            Console.WriteLine($"AtualizarConsulta chamado para ID: {consulta.Idconsulta}");
            _consultaService.AtualizarConsulta(consulta);
            Console.WriteLine("Consulta atualizada com sucesso");
            return NoContent();
        }

        // GET: api/Consulta/{id}
        [HttpGet("{id}")]
        public IActionResult BuscarConsulta(int id)
        {
            Console.WriteLine($"BuscarConsulta chamado para ID: {id}");
            var consulta = _consultaService.BuscarConsulta(id);
            if (consulta == null)
            {
                Console.WriteLine($"Consulta com ID {id} não encontrada");
                return NotFound();
            }
            Console.WriteLine($"Consulta encontrada: {consulta.Idconsulta}");
            return Ok(consulta);
        }

        // DELETE: api/Consulta/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletarConsulta(int id)
        {
            Console.WriteLine($"DeletarConsulta chamado para ID: {id}");
            _consultaService.DeletarConsulta(id);
            Console.WriteLine($"Consulta com ID {id} deletada");
            return NoContent();
        }
    }
}
