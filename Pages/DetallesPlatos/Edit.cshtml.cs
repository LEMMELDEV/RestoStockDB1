using RestoStockDB1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestoStockDB1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestoStockDB1.Pages.DetallesPlatos
{
    public class EditModel : PageModel
    {
        private readonly RestoStockContext _context;

        public EditModel(RestoStockContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DetallePlato DetallePlato { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
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

            DetallePlato = detallePlato;

            // Cargar las listas desplegables para Platos e Ingredientes
            ViewData["Platos"] = new SelectList(await _context.Platos.ToListAsync(), "PlatoId", "Nombre");
            ViewData["Ingredientes"] = new SelectList(await _context.Ingredientes.ToListAsync(), "IngredienteId", "Nombre");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Recargar las listas desplegables si el modelo no es válido
                ViewData["Platos"] = new SelectList(await _context.Platos.ToListAsync(), "PlatoId", "Nombre");
                ViewData["Ingredientes"] = new SelectList(await _context.Ingredientes.ToListAsync(), "IngredienteId", "Nombre");
                return Page();
            }

            _context.Attach(DetallePlato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetallePlatoExists(DetallePlato.DetallePlatoId))
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

        private bool DetallePlatoExists(int id)
        {
            return (_context.DetallesPlatos?.Any(e => e.DetallePlatoId == id)).GetValueOrDefault();
        }
    }
}
