using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using SMT.Models;


[ApiController]
[Route("api/[controller]")]
public class ConsultaDetalhadaController : ControllerBase
{
    private readonly ConsultaDetalhadaService _service;

    public ConsultaDetalhadaController(ConsultaDetalhadaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ConsultaDetalhada>>> Get()
    {
        var consultas = await _service.ObterConsultasDetalhadasAsync();
        return Ok(consultas);
    }
}

