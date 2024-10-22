using Microsoft.AspNetCore.Mvc;
using SMT.Models;
using System;

namespace SMT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicoController : ControllerBase
    {
        private readonly MedicoService _medicoService;

        public MedicoController(MedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        // POST: api/Medico
        [HttpPost]
        public IActionResult AdicionarMedico([FromBody] Medico medico)
        {
            Console.WriteLine("AdicionarMedico chamado");

            if (medico == null)
            {
                Console.WriteLine("Erro: Médico é nulo");
                return BadRequest("Médico não pode ser nulo.");
            }

            Console.WriteLine($"Adicionando médico: {medico.Crmmed}");
            _medicoService.AdicionarMedico(medico);
            return CreatedAtAction(nameof(BuscarMedico), new { crm = medico.Crmmed }, medico);
        }

        // PUT: api/Medico
        [HttpPut]
        public IActionResult AtualizarMedico([FromBody] Medico medico)
        {
            Console.WriteLine($"AtualizarMedico chamado para CRM: {medico.Crmmed}");
            _medicoService.AtualizarMedico(medico);
            Console.WriteLine("Médico atualizado com sucesso");
            return NoContent();
        }

        // GET: api/Medico/{crm}
        [HttpGet("{crm}")]
        public IActionResult BuscarMedico(string crm)
        {
            Console.WriteLine($"BuscarMedico chamado para CRM: {crm}");
            var medico = _medicoService.BuscarMedico(crm);
            if (medico == null)
            {
                Console.WriteLine($"Médico com CRM {crm} não encontrado");
                return NotFound();
            }
            Console.WriteLine($"Médico encontrado: {medico.Crmmed}");
            return Ok(medico);
        }

        // DELETE: api/Medico/{crm}
        [HttpDelete("{crm}")]
        public IActionResult DeletarMedico(string crm)
        {
            Console.WriteLine($"DeletarMedico chamado para CRM: {crm}");
            _medicoService.DeletarMedico(crm);
            Console.WriteLine($"Médico com CRM {crm} deletado");
            return NoContent();
        }
    }
}
