using SMT.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class UnidadeService
{
    private readonly ContextoBD _context;

    public UnidadeService(ContextoBD context)
    {
        _context = context;
    }

    // Método para adicionar uma unidade
    public void AdicionarUnidade(Unidade unidade)
    {
        _context.Unidades.Add(unidade);
        _context.SaveChanges();
    }

    // Método para atualizar uma unidade
    public void AtualizarUnidade(Unidade unidade)
    {
        var unidadeExistente = _context.Unidades
            .FirstOrDefault(u => u.Idunidade == unidade.Idunidade); // Usando o ID da unidade recebida

        if (unidadeExistente != null)
        {
            unidadeExistente.Nomeunidade = unidade.Nomeunidade;
            unidadeExistente.Cepuni = unidade.Cepuni;
            unidadeExistente.Logradourouni = unidade.Logradourouni;
            unidadeExistente.Numerouni = unidade.Numerouni;
            unidadeExistente.Bairrouni = unidade.Bairrouni;
            unidadeExistente.Cidadeuni = unidade.Cidadeuni;
            unidadeExistente.Estadouni = unidade.Estadouni;
            unidadeExistente.Paisuni = unidade.Paisuni;
            _context.SaveChanges();
        }
    }

    // Método para buscar uma unidade
    public Unidade? BuscarUnidade(int id) // Alterando para retornar uma Unidade?
    {
        return _context.Unidades.FirstOrDefault(u => u.Idunidade == id);
    }

    // Método para deletar uma unidade
    public void DeletarUnidade(int id)
    {
        var unidadeExistente = _context.Unidades
            .FirstOrDefault(u => u.Idunidade == id);

        if (unidadeExistente != null)
        {
            _context.Unidades.Remove(unidadeExistente);
            _context.SaveChanges();
        }
    }
}
