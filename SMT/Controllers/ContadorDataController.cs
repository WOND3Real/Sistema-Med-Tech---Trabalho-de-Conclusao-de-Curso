using Microsoft.AspNetCore.Mvc;

namespace SMT.Controllers{

    [Route("api/[controller]")]
    [ApiController]
    public class ContadorDataController : ControllerBase
    {
        private readonly ContadorDataService _contadorDataService;

        public ContadorDataController(ContadorDataService contadorDataService)
        {
            _contadorDataService = contadorDataService;
        }

        [HttpGet("consultas-contribuintes")]
        public async Task<ActionResult<ContadorData>> GetConsultasEContribuintes()
        {
            var resultado = await _contadorDataService.CallContadorDataService();

            if (resultado == null)
            {
                return NotFound();
            }

            return Ok(resultado);
        }
    }
}