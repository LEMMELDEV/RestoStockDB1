using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestoStockDB1.Models;
using RestoStockDB1.Data;

namespace RestoStockDB1.Pages.Ingredientes
{
    public class IndexModel : PageModel
    {
        private readonly RestoStockContext _context;

        public IndexModel(RestoStockContext context)
        {
            _context = context;
        }

        public IList<Ingrediente> Ingredientes { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Ingredientes != null)
            {
                Ingredientes = await _context.Ingredientes.ToListAsync();
            }
        }
    }
}
