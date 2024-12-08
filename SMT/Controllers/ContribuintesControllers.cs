using Microsoft.AspNetCore.Mvc;
using SMT.Models;

namespace SMT.Controllers{

[Route("api/[controller]")]
[ApiController]
    public class ContribuintesController : ControllerBase
    {
        private readonly ContribuintesService _contribuintesService;

        public ContribuintesController(ContribuintesService contribuintesService)
        {
            _contribuintesService = contribuintesService;
        }

        // GET: api/Contribuintes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contribuinte>>> GetContribuintes()
        {
            var contribuintes = await _contribuintesService.GetContribuintesAsync();

            if (contribuintes == null || !contribuintes.Any())
            {
                return NotFound();
            }

            return Ok(contribuintes);
        }
    }
}
