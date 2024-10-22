using SMT.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class ConsultaService
{
    private readonly ContextoBD _context;

    public ConsultaService(ContextoBD context)
    {
        _context = context;
    }

    // Método para adicionar uma consulta
    public void AdicionarConsulta(Consultum consulta)
    {
        _context.Consulta.Add(consulta);
        _context.SaveChanges();
    }

    // Método para atualizar uma consulta
    public void AtualizarConsulta(Consultum consulta)
    {
        var consultaExistente = _context.Consulta
            .FirstOrDefault(c => c.Idconsulta == consulta.Idconsulta); // Usando o ID da consulta recebida

        if (consultaExistente != null)
        {
            consultaExistente.PacienteCpfpaci = consulta.PacienteCpfpaci;
            consultaExistente.AtendenteCtpsatend = consulta.AtendenteCtpsatend;
            consultaExistente.MedicoCrmmed = consulta.MedicoCrmmed;
            consultaExistente.UnidadeIdunidade = consulta.UnidadeIdunidade;
            consultaExistente.EspecialidadeIdespecialidade = consulta.EspecialidadeIdespecialidade;
            consultaExistente.Dataconsul = consulta.Dataconsul;
            consultaExistente.Horaconsul = consulta.Horaconsul;
            consultaExistente.statusconsul = consulta.statusconsul;
            _context.SaveChanges();
        }
    }

    // Método para buscar uma consulta
    public Consultum? BuscarConsulta(int id) // Alterando para retornar uma Consultum?
    {
        return _context.Consulta.FirstOrDefault(c => c.Idconsulta == id);
    }

    // Método para deletar uma consulta
    public void DeletarConsulta(int id)
    {
        var consultaExistente = _context.Consulta
            .FirstOrDefault(c => c.Idconsulta == id);

        if (consultaExistente != null)
        {
            _context.Consulta.Remove(consultaExistente);
            _context.SaveChanges();
        }
    }
}
