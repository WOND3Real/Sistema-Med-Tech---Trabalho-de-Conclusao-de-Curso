using SMT.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class ContadorDataService
{
    private readonly ContextoBD _context;

    public ContadorDataService(ContextoBD context)
    {
        _context = context;
    }

    public async Task<ContadorData?> CallContadorDataService()
    {
        // Executa a consulta SQL assíncrona e aguarda o resultado
        var result = await _context
            .Set<ContadorData>()
            .FromSqlRaw("SELECT * FROM contar_consultas_e_contribuintes()")
            .FirstOrDefaultAsync();

        // Verifica se o resultado é nulo
        if (result == null)
        {
            return null;
        }

        // Retorna o objeto ContadorData
        return new ContadorData
        {
            TotalConsultas = result.TotalConsultas,
            ConsultasConcluidas = result.ConsultasConcluidas,
            ConsultasCanceladas = result.ConsultasCanceladas,
            TotalContribuintes = result.TotalContribuintes
        };
    }
}
