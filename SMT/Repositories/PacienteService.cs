using SMT.Models;
using Microsoft.EntityFrameworkCore;

public class PacienteService
{
    private readonly ContextoBD _context;

    public PacienteService(ContextoBD context)
    {
        _context = context;
    }

    // Método para adicionar um paciente
    public async Task<bool> AdicionarPaciente(Paciente paciente)
    {
        // Verifica se o paciente já existe
        if (await _context.Pacientes.AnyAsync(p => p.CpfPaci == paciente.CpfPaci))
        {
            return false; // O paciente já existe
        }

        await _context.Pacientes.AddAsync(paciente);
        await _context.SaveChangesAsync();
        return true; // Paciente adicionado com sucesso
    }


    // Método para atualizar um paciente
    public async Task<bool> AtualizarPaciente(Paciente paciente)
    {
        try
        {
            var pacienteExistente = await _context.Pacientes
                .FirstOrDefaultAsync(p => p.CpfPaci == paciente.CpfPaci);

            if (pacienteExistente != null)
            {
                pacienteExistente.NomePaci = paciente.NomePaci;
                pacienteExistente.SobrenomePaci = paciente.SobrenomePaci;
                pacienteExistente.NascimentoPaci = paciente.NascimentoPaci;
                pacienteExistente.generoPaci = paciente.generoPaci;
                pacienteExistente.EmailPaci = paciente.EmailPaci;
                pacienteExistente.TelefonePaci = paciente.TelefonePaci;
                pacienteExistente.SenhaPaci = paciente.SenhaPaci;

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            // Log do erro (ex.Message) ou outra ação apropriada
            return false;
        }
    }

    // Método para buscar um paciente
    public async Task<Paciente?> BuscarPaciente(string cpf)
    {
        try
        {
            return await _context.Pacientes.FirstOrDefaultAsync(p => p.CpfPaci == cpf);
        }
        catch (Exception ex)
        {
            // Log do erro (ex.Message) ou outra ação apropriada
            return null;
        }
    }

    // Método para deletar um paciente
    public async Task<bool> DeletarPaciente(string cpf)
    {
        try
        {
            var pacienteExistente = await _context.Pacientes
                .FirstOrDefaultAsync(p => p.CpfPaci == cpf);

            if (pacienteExistente != null)
            {
                _context.Pacientes.Remove(pacienteExistente);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            // Log do erro (ex.Message) ou outra ação apropriada
            return false;
        }
    }
}
