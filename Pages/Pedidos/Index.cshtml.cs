using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestoStockDB1.Models;
using RestoStockDB1.Data;

namespace RestoStockDB1.Pages.Pedidos
{
    public class IndexModel : PageModel
    {
        private readonly RestoStockContext _context;

        public IndexModel(RestoStockContext context)
        {
            _context = context;
        }

        public IList<Pedido> Pedidos { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Pedidos != null)
            {
                // Obtiene la lista de pedidos, incluyendo el proveedor, si es necesario
                Pedidos = await _context.Pedidos
                    .Include(p => p.Proovedor) // Incluye los proveedores asociados a los pedidos
                    .ToListAsync();
            }
        }
    }
}
