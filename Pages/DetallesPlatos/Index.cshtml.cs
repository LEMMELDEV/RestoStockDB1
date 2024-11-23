using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestoStockDB1.Models;
using RestoStockDB1.Data;

namespace RestoStockDB1.Pages.DetallesPlatos
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly RestoStockContext _context;

        public IndexModel(RestoStockContext context)
        {
            _context = context;
        }

        public IList<DetallePlato> DetallesPlatos { get; set; } = default!;

        public async Task OnGetAsync()
        {
            DetallesPlatos = await _context.DetallesPlatos
            .Include(d => d.Plato) // Cargar nombres de Plato
            .Include(d => d.Ingrediente) // Cargar nombres de Ingrediente
            .ToListAsync();
        }
    }
}