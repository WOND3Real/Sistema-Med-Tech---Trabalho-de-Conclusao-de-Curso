using SMT.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
    public void AtualizarAtendente(Atendente atendente)
    {
        var atendenteExistente = _context.Atendentes
            .FirstOrDefault(a => a.Ctpsatend == atendente.Ctpsatend); // Usando o CTPS do atendente recebido

        if (atendenteExistente != null)
        {
            atendenteExistente.Nomeatend = atendente.Nomeatend;
            atendenteExistente.Sobrenomeatend = atendente.Sobrenomeatend;
            atendenteExistente.Inicioturnoatend = atendente.Inicioturnoatend;
            atendenteExistente.Fimturnoatend = atendente.Fimturnoatend;
            atendenteExistente.Senhaatend = atendente.Senhaatend;
            _context.SaveChanges();
        }
    }

    // Método para buscar um atendente
    public Atendente? BuscarAtendente(string ctps) // Alterando para retornar um Atendente?
    {
        return _context.Atendentes.FirstOrDefault(a => a.Ctpsatend == ctps);
    }

    // Método para deletar um atendente
    public void DeletarAtendente(string ctps)
    {
        var atendenteExistente = _context.Atendentes
            .FirstOrDefault(a => a.Ctpsatend == ctps);

        if (atendenteExistente != null)
        {
            _context.Atendentes.Remove(atendenteExistente);
            _context.SaveChanges();
        }
    }
}
