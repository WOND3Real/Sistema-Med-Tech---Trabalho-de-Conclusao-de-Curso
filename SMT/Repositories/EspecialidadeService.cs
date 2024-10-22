using SMT.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class EspecialidadeService
{
    private readonly ContextoBD _context;

    public EspecialidadeService(ContextoBD context)
    {
        _context = context;
    }

    // Método para adicionar uma especialidade
    public void AdicionarEspecialidade(Especialidade especialidade)
    {
        _context.Especialidades.Add(especialidade);
        _context.SaveChanges();
    }

    // Método para atualizar uma especialidade
    public void AtualizarEspecialidade(Especialidade especialidade)
    {
        var especialidadeExistente = _context.Especialidades
            .FirstOrDefault(e => e.Idespecialidade == especialidade.Idespecialidade); // Usando o ID da especialidade recebida

        if (especialidadeExistente != null)
        {
            especialidadeExistente.Nomeespec = especialidade.Nomeespec; // Atualizando o nome da especialidade
            _context.SaveChanges();
        }
    }

    // Método para buscar uma especialidade
    public Especialidade? BuscarEspecialidade(int id) // Alterando para retornar uma Especialidade?
    {
        return _context.Especialidades.FirstOrDefault(e => e.Idespecialidade == id);
    }

    // Método para deletar uma especialidade
    public void DeletarEspecialidade(int id)
    {
        var especialidadeExistente = _context.Especialidades
            .FirstOrDefault(e => e.Idespecialidade == id);

        if (especialidadeExistente != null)
        {
            _context.Especialidades.Remove(especialidadeExistente);
            _context.SaveChanges();
        }
    }
}
