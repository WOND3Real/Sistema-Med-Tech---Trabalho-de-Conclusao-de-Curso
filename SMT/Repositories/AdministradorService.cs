using SMT.Models;
using Microsoft.EntityFrameworkCore;

public class AdministradorService
{
    private readonly ContextoBD _context;

    public AdministradorService(ContextoBD context)
    {
        _context = context;
    }

    // Método para adicionar um administrador
    public async Task<bool> AdicionarAdministrador(Administrador administrador)
    {
        try
        {
            await _context.Administradores.AddAsync(administrador);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            // Lida com erro
            throw new Exception($"Erro ao adicionar administrador: {ex.Message}", ex);
        }
    }

    // Método para atualizar um administrador
    public async Task<bool> AtualizarAdministrador(Administrador administrador)
    {
        try
        {
            var administradorExistente = await _context.Administradores
                .FirstOrDefaultAsync(a => a.IdAdministrador == administrador.IdAdministrador);

            if (administradorExistente != null)
            {
                administradorExistente.NomeAdm = administrador.NomeAdm;
                administradorExistente.SobrenomeAdm = administrador.SobrenomeAdm;
                administradorExistente.SenhaAdm = administrador.SenhaAdm;
                administradorExistente.UnidadeIdUnidade = administrador.UnidadeIdUnidade;

                await _context.SaveChangesAsync();
                return true;
            }

            return false; // Caso o administrador não seja encontrado
        }
        catch (Exception ex)
        {
            // Lida com erro
            throw new Exception($"Erro ao atualizar administrador: {ex.Message}", ex);
        }
    }

    // Método para buscar um administrador
    public async Task<Administrador?> BuscarAdministrador(int id)
    {
        try
        {
            return await _context.Administradores
                .FirstOrDefaultAsync(a => a.IdAdministrador == id);
        }
        catch (Exception ex)
        {
            // Lida com erro
            throw new Exception($"Erro ao buscar administrador: {ex.Message}", ex);
        }
    }

    // Método para deletar um administrador
    public async Task<bool> DeletarAdministrador(int id)
    {
        try
        {
            var administradorExistente = await _context.Administradores
                .FirstOrDefaultAsync(a => a.IdAdministrador == id);

            if (administradorExistente != null)
            {
                _context.Administradores.Remove(administradorExistente);
                await _context.SaveChangesAsync();
                return true;
            }

            return false; // Caso o administrador não seja encontrado
        }
        catch (Exception ex)
        {
            // Lida com erro
            throw new Exception($"Erro ao deletar administrador: {ex.Message}", ex);
        }
    }
}
