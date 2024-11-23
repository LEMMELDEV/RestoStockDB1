using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestoStockDB1.Data;
using RestoStockDB1.Models;

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

        public SelectList Platos { get; set; } = default!;
        public SelectList Ingredientes { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (_context.DetallesPlatos == null)
            {
                return NotFound();
            }

            DetallePlato = await _context.DetallesPlatos
                .Include(d => d.Plato)
                .Include(d => d.Ingrediente)
                .FirstOrDefaultAsync(m => m.DetallePlatoId == id);

            if (DetallePlato == null)
            {
                return NotFound();
            }

            // Cargar listas de Platos e Ingredientes para los dropdowns
            Platos = new SelectList(await _context.Platos.ToListAsync(), "PlatoId", "Nombre", DetallePlato.PlatoId);
            Ingredientes = new SelectList(await _context.Ingredientes.ToListAsync(), "IngredienteId", "Nombre", DetallePlato.IngredienteId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                //return Page();
            }

            _context.DetallesPlatos.Update(DetallePlato);

            try
            {
                // Imprimir información para depuración
                Console.WriteLine($"Guardando cambios para DetallePlatoId: {DetallePlato.DetallePlatoId}");

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
            return _context.DetallesPlatos.Any(e => e.DetallePlatoId == id);
        }
    }
}