using SMT.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class PacienteService
{
    private readonly ContextoBD _context;

    public PacienteService(ContextoBD context)
    {
        _context = context;
    }

    // Método para adicionar um paciente
    public void AdicionarPaciente(Paciente paciente)
    {
        _context.Pacientes.Add(paciente);
        _context.SaveChanges();
    }

    // Método para atualizar um paciente
    public void AtualizarPaciente(Paciente paciente)
    {
        var pacienteExistente = _context.Pacientes
            .FirstOrDefault(p => p.Cpfpaci == paciente.Cpfpaci);  // Usando o CPF do paciente recebido

        if (pacienteExistente != null)
        {
            pacienteExistente.Nomepaci = paciente.Nomepaci;
            pacienteExistente.Sobrenomepaci = paciente.Sobrenomepaci;
            pacienteExistente.Nascimentopaci = paciente.Nascimentopaci;
            pacienteExistente.genero = paciente.genero;
            pacienteExistente.Emailpaci = paciente.Emailpaci;
            pacienteExistente.Telefonepaci = paciente.Telefonepaci;
            pacienteExistente.Senhapaci = paciente.Senhapaci;
            _context.SaveChanges();
        }
    }

    // Método para buscar um paciente
    public Paciente? BuscarPaciente(string cpf) // Alterando para Paciente?
    {
        return _context.Pacientes.FirstOrDefault(p => p.Cpfpaci == cpf);
    }

    // Método para deletar um paciente
    public void DeletarPaciente(string cpf)
    {
        var pacienteExistente = _context.Pacientes
            .FirstOrDefault(p => p.Cpfpaci == cpf);

        if (pacienteExistente != null)
        {
            _context.Pacientes.Remove(pacienteExistente);
            _context.SaveChanges();
        }
    }

}
