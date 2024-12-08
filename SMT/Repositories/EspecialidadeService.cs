using SMT.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class EspecialidadeService
{
    private readonly ContextoBD _context;

    public EspecialidadeService(ContextoBD context)
    {
        _context = context;
    }

    // Método para buscar o id de uma especialidade pelo nome
    public async Task<int?> BuscarIdPorNome(string nomeEspecialidade)
    {
        try
        {
            var especialidade = await _context.Especialidades
                .FirstOrDefaultAsync(e => e.Nomeespec == nomeEspecialidade);

            return especialidade?.Idespecialidade;
        }
        catch (Exception ex)
        {
            // Lida com erro
            throw new Exception($"Erro ao buscar o id da especialidade: {ex.Message}", ex);
        }
    }
}
