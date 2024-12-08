using Microsoft.AspNetCore.Mvc;
using SMT.Models;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

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
    
        // GET: api/Administrador/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarAdministrador(int id)
        {
            try
            {
                var administrador = await _administradorService.BuscarAdministrador(id);
                if (administrador == null)
                {
                    return NotFound($"Administrador com ID {id} não encontrado.");
                }
                return Ok(administrador);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar administrador: {ex.Message}");
            }
        }
    }
}
