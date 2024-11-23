using RestoStockDB1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestoStockDB1.Models;

namespace RestoStockDB1.Pages.Pedidos
{
    public class DeleteModel : PageModel
    {
        private readonly RestoStockContext _context;

        public DeleteModel(RestoStockContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pedido Pedido { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Proovedor)
                .FirstOrDefaultAsync(m => m.PedidoId == id);

            if (pedido == null)
            {
                return NotFound();
            }
            else
            {
                Pedido = pedido;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido != null)
            {
                Pedido = pedido;
                _context.Pedidos.Remove(Pedido);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
