using SMT.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

public class ConsultaService
{
    private readonly ContextoBD _context;

    public ConsultaService(ContextoBD context)
    {
        _context = context;
    }

    // Método para adicionar uma consulta
    public async Task<bool> AdicionarConsulta(Consultum consulta)
    {
        try
        {
            await _context.Consulta.AddAsync(consulta);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            // Lida com erro
            throw new Exception($"Erro ao adicionar consulta: {ex.Message}", ex);
        }
    }

    // Método para atualizar uma consulta
    public async Task<bool> AtualizarConsulta(Consultum consulta)
    {
        try
        {
            var consultaExistente = await _context.Consulta
                .FirstOrDefaultAsync(c => c.IdConsulta == consulta.IdConsulta);

            if (consultaExistente != null)
            {
                consultaExistente.PacienteCpfPaci = consulta.PacienteCpfPaci;
                consultaExistente.AtendenteCtpsAtend = consulta.AtendenteCtpsAtend;
                consultaExistente.MedicoCrmMed = consulta.MedicoCrmMed;
                consultaExistente.UnidadeIdUnidade = consulta.UnidadeIdUnidade;
                consultaExistente.EspecialidadeIdEspecialidade = consulta.EspecialidadeIdEspecialidade;
                consultaExistente.DataConsul = consulta.DataConsul;
                consultaExistente.HoraConsul = consulta.HoraConsul;
                consultaExistente.StatusConsul = consulta.StatusConsul;

                await _context.SaveChangesAsync();
                return true;
            }
            return false; // Caso a consulta não seja encontrada
        }
        catch (Exception ex)
        {
            // Lida com erro
            throw new Exception($"Erro ao atualizar consulta: {ex.Message}", ex);
        }
    }

    // Método para buscar uma consulta
    public async Task<Consultum?> BuscarConsulta(int id)
    {
        try
        {
            return await _context.Consulta
                .FirstOrDefaultAsync(c => c.IdConsulta == id);
        }
        catch (Exception ex)
        {
            // Lida com erro
            throw new Exception($"Erro ao buscar consulta: {ex.Message}", ex);
        }
    }

    // Método para deletar uma consulta
    public async Task<bool> DeletarConsulta(int id)
    {
        try
        {
            var consultaExistente = await _context.Consulta
                .FirstOrDefaultAsync(c => c.IdConsulta == id);

            if (consultaExistente != null)
            {
                _context.Consulta.Remove(consultaExistente);
                await _context.SaveChangesAsync();
                return true;
            }
            return false; // Caso a consulta não seja encontrada
        }
        catch (Exception ex)
        {
            // Lida com erro
            throw new Exception($"Erro ao deletar consulta: {ex.Message}", ex);
        }
    }
}
