using RestoStockDB1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestoStockDB1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestoStockDB1.Pages.Platos
{
    public class EditModel : PageModel
    {
        private readonly RestoStockContext _context;
        public EditModel(RestoStockContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Plato Plato { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Platos == null)
            {
                return NotFound();
            }
            var plato = await _context.Platos.FirstOrDefaultAsync(m => m.PlatoId == id);
            if (plato == null)
            {
                return NotFound();
            }
            else
            {
                Plato = plato;
                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                //return Page(); // Si el modelo no es válido, vuelve a la misma página
            }
            _context.Attach(Plato).State = EntityState.Modified; // Marca la entidad como modificada
            try
            {
                await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
            }
            catch (DbUpdateConcurrencyException) // Si ocurre un error de concurrencia
            {
                if (!PlatosExists(Plato.PlatoId)) // Si el plato ya no existe
                {
                    return NotFound(); // Devuelve un error 404
                }
                else
                {
                    throw; // Re lanza la excepción para que sea manejada por niveles superiores
                }
            }
            return RedirectToPage("./Index"); // Redirige a la página de índice
        }
        private bool PlatosExists(int id)
        {
            return (_context.Platos?.Any(e => e.PlatoId == id)).GetValueOrDefault();
        }
    }
}
