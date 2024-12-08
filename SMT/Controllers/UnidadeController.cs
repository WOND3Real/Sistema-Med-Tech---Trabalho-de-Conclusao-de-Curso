using Microsoft.AspNetCore.Mvc;
using SMT.Models;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadeController : ControllerBase
    {
        private readonly UnidadeService _unidadeService;

        public UnidadeController(UnidadeService unidadeService)
        {
            _unidadeService = unidadeService;
        }

        // GET: api/Unidade
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Unidade>>> GetUnidades()
        {
            var unidades = await _unidadeService.GetAllAsync();
            return Ok(unidades);
        }
    }
}
