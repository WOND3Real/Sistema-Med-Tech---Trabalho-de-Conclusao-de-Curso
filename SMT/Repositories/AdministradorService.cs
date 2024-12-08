using SMT.Models;
using Microsoft.EntityFrameworkCore;

public class AdministradorService
{
    private readonly ContextoBD _context;

    public AdministradorService(ContextoBD context)
    {
        _context = context;
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
}
