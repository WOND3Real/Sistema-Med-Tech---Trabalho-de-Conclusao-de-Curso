using SMT.Models;
using Microsoft.EntityFrameworkCore;

public class ContribuintesService
{
    private readonly ContextoBD _context;

    public ContribuintesService(ContextoBD context){
        _context = context;
    }

    public async Task<IEnumerable<Contribuinte>> GetContribuintesAsync()
    {
        var contribuintes = await _context.Contribuintes
            .FromSqlRaw("select * from contribuintes;")
            .ToListAsync();
        return contribuintes;
    }
}
