using SMT.Models;
using Microsoft.EntityFrameworkCore;

    public class UnidadeService
    {
        private readonly ContextoBD _context;

        public UnidadeService(ContextoBD context)
        {
            _context = context;
        }

        public async Task<List<Unidade>> GetAllAsync()
        {
            return await _context.Unidade.ToListAsync();
        }
    }

