using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestoStockDB1.Data;
using RestoStockDB1.Models;

namespace RestoStockDB1.Pages.Pedidos
{
    public class EditModel : PageModel
    {
        private readonly RestoStockContext _context;

        public EditModel(RestoStockContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pedido Pedido { get; set; } = default!;
        public SelectList ProovedoresSelectList { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Proovedor)
                .FirstOrDefaultAsync(p => p.PedidoId == id);

            if (pedido == null)
            {
                return NotFound();
            }

            ProovedoresSelectList = new SelectList(await _context.Proovedores.ToListAsync(), "ProovedorId", "NombreEmpresa");

            Pedido = pedido;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ProovedoresSelectList = new SelectList(await _context.Proovedores.ToListAsync(), "ProovedorId", "NombreEmpresa");
                return Page();
            }

            _context.Attach(Pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(Pedido.PedidoId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PedidoExists(int id)
        {
            return (_context.Pedidos?.Any(e => e.PedidoId == id)).GetValueOrDefault();
        }
    }
}