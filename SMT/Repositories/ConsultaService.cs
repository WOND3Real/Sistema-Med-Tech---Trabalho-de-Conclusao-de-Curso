using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SMT.Models;

public class ConsultaService
{
    private readonly ContextoBD _context;

    public ConsultaService(ContextoBD context)
    {
        _context = context;
    }

    public async Task<bool> AdicionarConsultaAsync(Consulta consulta)
    {
        try
        {
            // Verificar se já existe uma consulta com os mesmos dados para evitar duplicação
            var consultaExistente = await _context.Consulta
                .AnyAsync(c => c.PacienteCpfPaci == consulta.PacienteCpfPaci
                               && c.AtendenteCtpsatend == consulta.AtendenteCtpsatend
                               && c.MedicoCrmmed == consulta.MedicoCrmmed
                               && c.UnidadeIdunidade == consulta.UnidadeIdunidade
                               && c.EspecialidadeIdespecialidade == consulta.EspecialidadeIdespecialidade
                               && c.DataConsul == consulta.DataConsul
                               && c.HoraConsul == consulta.HoraConsul);

            // Logando o resultado da verificação
            Console.WriteLine($"Verificando duplicação da consulta: {consultaExistente}");

            if (consultaExistente)
            {
                // Se a consulta já existir, logar a falha e retornar false
                Console.WriteLine("Consulta já existe. Não será adicionada.");
                return false; // Retorna false se a consulta já existir
            }

            // Adicionar a nova consulta
            await _context.Consulta.AddAsync(consulta);
            await _context.SaveChangesAsync();

            // Log de sucesso
            Console.WriteLine($"Consulta adicionada com sucesso: {consulta.PacienteCpfPaci} - {consulta.DataConsul}");

            return true; // Retorna true se a consulta for adicionada com sucesso
        }
        catch (DbUpdateException dbEx)
        {
            // Log detalhado de erro para DbUpdateException
            Console.WriteLine($"Erro ao acessar o banco de dados: {dbEx.Message}");
            Console.WriteLine($"Detalhes do erro: {dbEx.InnerException?.Message}");
            return false;
        }
        catch (Exception ex)
        {
            // Log detalhado para qualquer outro tipo de exceção
            Console.WriteLine($"Erro inesperado: {ex.Message}");
            Console.WriteLine($"Detalhes do erro: {ex.StackTrace}");
            return false;
        }
    }

    public async Task<Consulta> BuscarConsultaPorIdAsync(int id)
    {
        try
        {
            var consulta = await _context.Consulta.FindAsync(id);

            // Verifica se a consulta foi encontrada
            if (consulta == null)
            {
                Console.WriteLine($"Consulta não encontrada com o ID: {id}");
                return null;
            }

            // Log para sucesso na busca
            Console.WriteLine($"Consulta encontrada: {consulta.PacienteCpfPaci} - {consulta.DataConsul}");
            return consulta;
        }
        catch (Exception ex)
        {
            // Log de erro ao buscar consulta por ID
            Console.WriteLine($"Erro ao buscar consulta por ID: {ex.Message}");
            return null;
        }
    }
}
