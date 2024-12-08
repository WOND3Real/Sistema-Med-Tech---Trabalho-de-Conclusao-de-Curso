using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SMT.Models;

public class ConsultaDetalhadaService
{
    private readonly ContextoBD _context;

    public ConsultaDetalhadaService(ContextoBD context)
    {
        _context = context;
    }

    public async Task<List<ConsultaDetalhada>> ObterConsultasDetalhadasAsync()
    {
        return await _context.Set<ConsultaDetalhada>().ToListAsync();
    }
}

