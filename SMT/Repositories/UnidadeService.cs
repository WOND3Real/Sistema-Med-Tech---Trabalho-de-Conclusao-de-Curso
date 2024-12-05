using SMT.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class UnidadeService
{
    private readonly ContextoBD _context;

    public UnidadeService(ContextoBD context)
    {
        _context = context;
    }

    // Método para adicionar uma unidade
    public async Task<bool> AdicionarUnidade(Unidade unidade)
    {
        try
        {
            await _context.Unidades.AddAsync(unidade);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            // Lida com erro
            throw new Exception($"Erro ao adicionar unidade: {ex.Message}", ex);
        }
    }

    // Método para atualizar uma unidade
    public async Task<bool> AtualizarUnidade(Unidade unidade)
    {
        try
        {
            var unidadeExistente = await _context.Unidades
                .FirstOrDefaultAsync(u => u.Idunidade == unidade.Idunidade);

            if (unidadeExistente != null)
            {
                unidadeExistente.Nomeunidade = unidade.Nomeunidade;
                unidadeExistente.Cepuni = unidade.Cepuni;
                unidadeExistente.Logradourouni = unidade.Logradourouni;
                unidadeExistente.Numerouni = unidade.Numerouni;
                unidadeExistente.Bairrouni = unidade.Bairrouni;
                unidadeExistente.Cidadeuni = unidade.Cidadeuni;
                unidadeExistente.Estadouni = unidade.Estadouni;
                unidadeExistente.Paisuni = unidade.Paisuni;

                await _context.SaveChangesAsync();
                return true;
            }
            return false; // Caso a unidade não seja encontrada
        }
        catch (Exception ex)
        {
            // Lida com erro
            throw new Exception($"Erro ao atualizar unidade: {ex.Message}", ex);
        }
    }

    // Método para buscar uma unidade
    public async Task<Unidade?> BuscarUnidade(int id)
    {
        try
        {
            return await _context.Unidades
                .FirstOrDefaultAsync(u => u.Idunidade == id);
        }
        catch (Exception ex)
        {
            // Lida com erro
            throw new Exception($"Erro ao buscar unidade: {ex.Message}", ex);
        }
    }

    // Método para deletar uma unidade
    public async Task<bool> DeletarUnidade(int id)
    {
        try
        {
            var unidadeExistente = await _context.Unidades
                .FirstOrDefaultAsync(u => u.Idunidade == id);

            if (unidadeExistente != null)
            {
                _context.Unidades.Remove(unidadeExistente);
                await _context.SaveChangesAsync();
                return true;
            }
            return false; // Caso a unidade não seja encontrada
        }
        catch (Exception ex)
        {
            // Lida com erro
            throw new Exception($"Erro ao deletar unidade: {ex.Message}", ex);
        }
    }
}
