using SMT.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class AdministradorService
{
    private readonly ContextoBD _context;

    public AdministradorService(ContextoBD context)
    {
        _context = context;
    }

    // Método para adicionar um administrador
    public void AdicionarAdministrador(Administrador administrador)
    {
        _context.Administradores.Add(administrador);
        _context.SaveChanges();
    }

    // Método para atualizar um administrador
    public void AtualizarAdministrador(Administrador administrador)
    {
        var administradorExistente = _context.Administradores
            .FirstOrDefault(a => a.Idadministrador == administrador.Idadministrador); // Usando o ID do administrador recebido

        if (administradorExistente != null)
        {
            administradorExistente.Nomeadm = administrador.Nomeadm;
            administradorExistente.Sobrenomeadm = administrador.Sobrenomeadm;
            administradorExistente.Senhaadm = administrador.Senhaadm;
            // A unidade pode ser atualizada se necessário
            administradorExistente.UnidadeIdunidade = administrador.UnidadeIdunidade; 
            _context.SaveChanges();
        }
    }

    // Método para buscar um administrador
    public Administrador? BuscarAdministrador(int id) // Alterando para retornar um Administrador?
    {
        return _context.Administradores.FirstOrDefault(a => a.Idadministrador == id);
    }

    // Método para deletar um administrador
    public void DeletarAdministrador(int id)
    {
        var administradorExistente = _context.Administradores
            .FirstOrDefault(a => a.Idadministrador == id);

        if (administradorExistente != null)
        {
            _context.Administradores.Remove(administradorExistente);
            _context.SaveChanges();
        }
    }
}
