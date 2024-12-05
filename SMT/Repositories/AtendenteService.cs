using SMT.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class AtendenteService
{
    private readonly ContextoBD _context;

    public AtendenteService(ContextoBD context)
    {
        _context = context;
    }

    // Método para adicionar um atendente
    public void AdicionarAtendente(Atendente atendente)
    {
        _context.Atendentes.Add(atendente);
        _context.SaveChanges();
    }

    // Método para atualizar um atendente
    public async Task<bool> AtualizarAtendente(Atendente atendente)
    {
        try
        {
            // Buscar o atendente existente de forma assíncrona
            var atendenteExistente = await _context.Atendentes
                .FirstOrDefaultAsync(a => a.Ctpsatend == atendente.Ctpsatend);

            if (atendenteExistente != null)
            {
                atendenteExistente.Nomeatend = atendente.Nomeatend;
                atendenteExistente.Sobrenomeatend = atendente.Sobrenomeatend;
                atendenteExistente.Inicioturnoatend = atendente.Inicioturnoatend;
                atendenteExistente.Fimturnoatend = atendente.Fimturnoatend;
                atendenteExistente.Senhaatend = atendente.Senhaatend;

                await _context.SaveChangesAsync();
                return true;
            }
            return false; // Retorna falso se o atendente não for encontrado
        }
        catch
        {
            return false; // Retorna falso em caso de erro
        }
    }

    // Método para buscar um atendente
    public async Task<Atendente?> BuscarAtendente(string ctps)
    {
        return await _context.Atendentes
            .FirstOrDefaultAsync(a => a.Ctpsatend == ctps);
    }

    // Método para deletar um atendente
    public async Task<bool> DeletarAtendente(string ctps)
    {
        try
        {
            var atendenteExistente = await _context.Atendentes
                .FirstOrDefaultAsync(a => a.Ctpsatend == ctps);

            if (atendenteExistente != null)
            {
                _context.Atendentes.Remove(atendenteExistente);
                await _context.SaveChangesAsync();
                return true;
            }
            return false; // Retorna falso se o atendente não for encontrado
        }
        catch
        {
            return false; // Retorna falso em caso de erro
        }
    }
}
