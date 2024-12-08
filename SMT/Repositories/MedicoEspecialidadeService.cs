using Microsoft.EntityFrameworkCore;
using SMT.Models;

public class MedicoEspecialidadeService
{
    private readonly ContextoBD _context;

    public MedicoEspecialidadeService(ContextoBD context)
    {
        _context = context;
    }

    public async Task<List<MedicoEspecialidade>> GetMedicosEspecialidadesByUnidadeIdAsync(int unidadeId)
    {
        return await _context.MedicosEspecialidades.Where(me => me.UnidadeId == unidadeId).ToListAsync();
    }
}
