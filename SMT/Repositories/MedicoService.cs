using SMT.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class MedicoService
{
    private readonly ContextoBD _context;

    public MedicoService(ContextoBD context)
    {
        _context = context;
    }

    // Método para adicionar um médico
    public void AdicionarMedico(Medico medico)
    {
        _context.Medicos.Add(medico);
        _context.SaveChanges();
    }

    // Método para atualizar um médico
    public void AtualizarMedico(Medico medico)
    {
        var medicoExistente = _context.Medicos
            .FirstOrDefault(m => m.Crmmed == medico.Crmmed); // Usando o CRM do médico recebido

        if (medicoExistente != null)
        {
            medicoExistente.Nomemed = medico.Nomemed;
            medicoExistente.Sobrenomemed = medico.Sobrenomemed;
            medicoExistente.Telefonemed = medico.Telefonemed;
            medicoExistente.Emailmed = medico.Emailmed;
            medicoExistente.Senhamed = medico.Senhamed;
            // A lista de especialidades e unidades pode ser atualizada se necessário
            _context.SaveChanges();
        }
    }

    // Método para buscar um médico
    public Medico? BuscarMedico(string crm) // Alterando para retornar um Medico?
    {
        return _context.Medicos.FirstOrDefault(m => m.Crmmed == crm);
    }

    // Método para deletar um médico
    public void DeletarMedico(string crm)
    {
        var medicoExistente = _context.Medicos
            .FirstOrDefault(m => m.Crmmed == crm);

        if (medicoExistente != null)
        {
            _context.Medicos.Remove(medicoExistente);
            _context.SaveChanges();
        }
    }
}
