using SMT.Models;
using Microsoft.EntityFrameworkCore;

public class MedicoService
{
    private readonly ContextoBD _context;

    public MedicoService(ContextoBD context)
    {
        _context = context;
    }

    // Método para adicionar um médico
    public bool AdicionarMedico(Medico medico)
    {
        try
        {
            Console.WriteLine("Iniciando o método AdicionarMedico...");

            using (var context = new ContextoBD())
            {
                // Verifica se o médico já existe
                var medicoExistente = context.Medicos
                    .FirstOrDefault(m => m.Crmmed == medico.Crmmed);

                if (medicoExistente != null)
                {
                    Console.WriteLine("Médico já existe no banco.");
                    return false;
                }

                Console.WriteLine("Médico não encontrado. Tentando adicionar...");

                // Adiciona o médico
                context.Medicos.Add(medico);
                var linhasAfetadas = context.SaveChanges();

                Console.WriteLine($"Linhas afetadas: {linhasAfetadas}");
                return linhasAfetadas > 0;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro capturado: {ex.Message}");
            return false;
        }
    }



    // Método para atualizar um médico
    public async Task<bool> AtualizarMedico(Medico medico)
    {
        try
        {
            var medicoExistente = await _context.Medicos
                .FirstOrDefaultAsync(m => m.Crmmed == medico.Crmmed);

            if (medicoExistente != null)
            {
                medicoExistente.Nomemed = medico.Nomemed;
                medicoExistente.Sobrenomemed = medico.Sobrenomemed;
                medicoExistente.Telefonemed = medico.Telefonemed;
                medicoExistente.Emailmed = medico.Emailmed;
                medicoExistente.Senhamed = medico.Senhamed;

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



    // Método para buscar um médico
    public async Task<Medico?> BuscarMedico(string crm)
    {
        try
        {
            return await _context.Medicos.FirstOrDefaultAsync(m => m.Crmmed == crm);
        }
        catch (Exception ex)
        {
            // Log do erro (ex.Message) ou outra ação apropriada
            return null;
        }
    }



    // Método para deletar um médico
    public async Task<bool> DeletarMedico(string crm)
    {
        try
        {
            Console.WriteLine($"Tentando deletar médico com CRM: {crm}");

            var medicoExistente = await _context.Medicos.FirstOrDefaultAsync(m => m.Crmmed == crm);
            if (medicoExistente == null)
            {
                Console.WriteLine($"Médico com CRM {crm} não encontrado.");
                return false;
            }

            Console.WriteLine($"Médico com CRM {crm} encontrado. Deletando...");

            _context.Medicos.Remove(medicoExistente);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Médico com CRM {crm} deletado com sucesso.");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao deletar médico: {ex.Message}");
            return false;
        }
    }

}
