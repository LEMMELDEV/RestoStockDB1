using RestoStockDB1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestoStockDB1.Models;

namespace RestoStockDB1.Pages.DetallesPlatos
{
    public class DeleteModel : PageModel
    {
        private readonly RestoStockContext _context;

        public DeleteModel(RestoStockContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DetallePlato DetallePlato { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DetallesPlatos == null)
            {
                return NotFound();
            }

            var detallePlato = await _context.DetallesPlatos
                .Include(dp => dp.Plato)
                .Include(dp => dp.Ingrediente)
                .FirstOrDefaultAsync(m => m.DetallePlatoId == id);

            if (detallePlato == null)
            {
                return NotFound();
            }
            else
            {
                DetallePlato = detallePlato;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.DetallesPlatos == null)
            {
                return NotFound();
            }

            var detallePlato = await _context.DetallesPlatos.FindAsync(id);

            if (detallePlato != null)
            {
                DetallePlato = detallePlato;
                _context.DetallesPlatos.Remove(DetallePlato);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
