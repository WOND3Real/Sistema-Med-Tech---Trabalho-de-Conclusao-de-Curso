using Microsoft.AspNetCore.Mvc;
using SMT.Models;
using System;

namespace SMT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministradorController : ControllerBase
    {
        private readonly AdministradorService _administradorService;

        public AdministradorController(AdministradorService administradorService)
        {
            _administradorService = administradorService;
        }

        // POST: api/Administrador
        [HttpPost]
        public IActionResult AdicionarAdministrador([FromBody] Administrador administrador)
        {
            Console.WriteLine("AdicionarAdministrador chamado");

            if (administrador == null)
            {
                Console.WriteLine("Erro: Administrador é nulo");
                return BadRequest("Administrador não pode ser nulo.");
            }

            Console.WriteLine($"Adicionando administrador: {administrador.Idadministrador}");
            _administradorService.AdicionarAdministrador(administrador);
            return CreatedAtAction(nameof(BuscarAdministrador), new { id = administrador.Idadministrador }, administrador);
        }

        // PUT: api/Administrador
        [HttpPut]
        public IActionResult AtualizarAdministrador([FromBody] Administrador administrador)
        {
            Console.WriteLine($"AtualizarAdministrador chamado para ID: {administrador.Idadministrador}");
            _administradorService.AtualizarAdministrador(administrador);
            Console.WriteLine("Administrador atualizado com sucesso");
            return NoContent();
        }

        // GET: api/Administrador/{id}
        [HttpGet("{id}")]
        public IActionResult BuscarAdministrador(int id)
        {
            Console.WriteLine($"BuscarAdministrador chamado para ID: {id}");
            var administrador = _administradorService.BuscarAdministrador(id);
            if (administrador == null)
            {
                Console.WriteLine($"Administrador com ID {id} não encontrado");
                return NotFound();
            }
            Console.WriteLine($"Administrador encontrado: {administrador.Idadministrador}");
            return Ok(administrador);
        }

        // DELETE: api/Administrador/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletarAdministrador(int id)
        {
            Console.WriteLine($"DeletarAdministrador chamado para ID: {id}");
            _administradorService.DeletarAdministrador(id);
            Console.WriteLine($"Administrador com ID {id} deletado");
            return NoContent();
        }
    }
}
