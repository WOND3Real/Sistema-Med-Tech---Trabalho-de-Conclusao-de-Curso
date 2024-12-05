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

    // Método para adicionar uma especialidade
    public async Task<bool> AdicionarEspecialidade(Especialidade especialidade)
    {
        try
        {
            await _context.Especialidades.AddAsync(especialidade);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            // Lida com erro
            throw new Exception($"Erro ao adicionar especialidade: {ex.Message}", ex);
        }
    }

    // Método para atualizar uma especialidade
    public async Task<bool> AtualizarEspecialidade(Especialidade especialidade)
    {
        try
        {
            var especialidadeExistente = await _context.Especialidades
                .FirstOrDefaultAsync(e => e.Idespecialidade == especialidade.Idespecialidade);

            if (especialidadeExistente != null)
            {
                especialidadeExistente.Nomeespec = especialidade.Nomeespec;
                await _context.SaveChangesAsync();
                return true;
            }
            return false; // Caso a especialidade não seja encontrada
        }
        catch (Exception ex)
        {
            // Lida com erro
            throw new Exception($"Erro ao atualizar especialidade: {ex.Message}", ex);
        }
    }

    // Método para buscar uma especialidade
    public async Task<Especialidade?> BuscarEspecialidade(object id)
    {
        try
        {
            if (id is int idInt)
            {
                // Tratar como int
                return await _context.Especialidades
                    .FirstOrDefaultAsync(e => e.Idespecialidade == idInt);
            }
            else if (id is string idString)
            {
                // Tratar como string (exemplo: procurar por nome ou outro campo)
                return await _context.Especialidades
                    .FirstOrDefaultAsync(e => e.Nomeespec == idString);  // Supondo que você tenha o campo 'Nome'
            }
            else
            {
                throw new ArgumentException("O parâmetro id deve ser um número ou uma string.");
            }
        }
        catch (Exception ex)
        {
            // Lida com erro
            throw new Exception($"Erro ao buscar especialidade: {ex.Message}", ex);
        }
    }


    // Método para deletar uma especialidade
    public async Task<bool> DeletarEspecialidade(int id)
    {
        try
        {
            var especialidadeExistente = await _context.Especialidades
                .FirstOrDefaultAsync(e => e.Idespecialidade == id);

            if (especialidadeExistente != null)
            {
                _context.Especialidades.Remove(especialidadeExistente);
                await _context.SaveChangesAsync();
                return true;
            }
            return false; // Caso a especialidade não seja encontrada
        }
        catch (Exception ex)
        {
            // Lida com erro
            throw new Exception($"Erro ao deletar especialidade: {ex.Message}", ex);
        }
    }
}
