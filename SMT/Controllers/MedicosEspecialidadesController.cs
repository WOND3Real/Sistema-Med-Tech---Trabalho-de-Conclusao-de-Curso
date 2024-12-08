using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class MedicosEspecialidadesController : ControllerBase
{
    private readonly MedicoEspecialidadeService _medicoEspecialidadeService;

    // Construtor
    public MedicosEspecialidadesController(MedicoEspecialidadeService medicoEspecialidadeService)
    {
        _medicoEspecialidadeService = medicoEspecialidadeService;
    }

    // Ação (método GET)
    [HttpGet("unidade/{unidadeId}")]
    public async Task<IActionResult> GetMedicosEspecialidadesByUnidadeId(int unidadeId)
    {
        var medicosEspecialidades = await _medicoEspecialidadeService.GetMedicosEspecialidadesByUnidadeIdAsync(unidadeId);
        if (medicosEspecialidades == null || !medicosEspecialidades.Any())
        {
            return NotFound();
        }

        return Ok(medicosEspecialidades);
    }
}
