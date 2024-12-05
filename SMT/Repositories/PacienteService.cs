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
        try
        {
            Console.WriteLine("Iniciando o método AdicionarPaciente...");

            // Verifica se o paciente já existe
            var pacienteExistente = await _context.Pacientes
                .FirstOrDefaultAsync(p => p.CpfPaci == paciente.CpfPaci);

            if (pacienteExistente != null)
            {
                Console.WriteLine("Paciente já existe no banco.");
                return false;
            }

            Console.WriteLine("Paciente não encontrado. Tentando adicionar...");

            // Adiciona o paciente
            _context.Pacientes.Add(paciente);
            var linhasAfetadas = await _context.SaveChangesAsync();

            Console.WriteLine($"Linhas afetadas: {linhasAfetadas}");
            return linhasAfetadas > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro capturado: {ex.Message}");
            return false;
        }
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
                pacienteExistente.GeneroPaci = paciente.GeneroPaci;
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
