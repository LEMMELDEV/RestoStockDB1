using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestoStockDB1.Data;
using RestoStockDB1.Models;

namespace RestoStockDB.Pages.Proovedores
{
    public class DeleteModel : PageModel
    {
        private readonly RestoStockContext _context;
        public DeleteModel(RestoStockContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            // Verifica si el id es nulo o si el contexto de proovedores es nulo.
            if (id == null || _context.Proovedores == null)
            {
                // Si alguna de las condiciones anteriores es verdadera, se devuelve un error 404 (Not Found).
                return NotFound();
            }
            // Busca el Proovedor con el id proporcionado de forma asíncrona.
            var proovedores = await _context.Proovedores.FindAsync(id);
            // Si el Proovedor no se encuentra, se devuelve un error 404.
            if (proovedores == null)
            {
                return NotFound();
            }
            // Si se encuentra el Proovedor, se procede a eliminarlo.
            Proovedor = proovedores; // Asigna el Proovedor encontrado a la propiedad Proovedor.
            _context.Proovedores.Remove(Proovedor); // Elimina el Proovedor del contexto.
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos de forma asíncrona.
            // Redirige a la página de índice después de eliminar el Proovedor.
            return RedirectToPage("./Index");
        }
    }
}

