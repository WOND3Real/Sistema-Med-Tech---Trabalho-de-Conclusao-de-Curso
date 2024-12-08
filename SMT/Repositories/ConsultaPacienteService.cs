using SMT.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

public class ConsultaPacienteService
{
    private readonly ContextoBD _context;

    public ConsultaPacienteService(ContextoBD context)
    {
        _context = context;
    }

    // Método para buscar uma consulta
    public async Task<ConsultaPaciente?> BuscarConsulta(int idConsulta)
    {
        try
        {
            return await _context.ConsultaPacientes
                .FirstOrDefaultAsync(c => c.IdConsulta == idConsulta);
        }
        catch (Exception ex)
        {
            // Lida com erro
            throw new Exception($"Erro ao buscar consulta: {ex.Message}", ex);
        }
    }
}
