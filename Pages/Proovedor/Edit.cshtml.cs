using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestoStockDB1.Data;
using RestoStockDB1.Models;

namespace RestoStockDB.Pages.Proovedores
{
    public class EditModel : PageModel
    {

        private readonly RestoStockContext _context;
        public EditModel(RestoStockContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Proovedor Proovedor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Proovedores == null)
            {
                return NotFound();
            }
            var proovedores = await _context.Proovedores.FirstOrDefaultAsync(m => m.ProovedorId == id);
            if (proovedores == null)
            {
                return NotFound();
            }
            else
            {
                Proovedor = proovedores;
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                //return Page(); // Si el modelo no es válido, vuelve a la misma página
            }
            _context.Attach(Proovedor).State = EntityState.Modified; // Marca la entidad como modificada
            try
            {
                await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
            }
            catch (DbUpdateConcurrencyException) // Si ocurre un error de concurrencia
            {
                if (!ProovedorExists(Proovedor.ProovedorId)) // Si el proovedor ya no existe
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

        private bool ProovedorExists(int id)
        {
            return (_context.Proovedores?.Any(e => e.ProovedorId == id)).GetValueOrDefault();
        }
    }
}
