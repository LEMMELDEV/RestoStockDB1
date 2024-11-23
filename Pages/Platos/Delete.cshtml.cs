using RestoStockDB1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestoStockDB1.Models;

namespace RestoStockDB1.Pages.Platos
{
    public class DeleteModel : PageModel
    {
        private readonly RestoStockContext _context;
        public DeleteModel(RestoStockContext context)
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

            Plato = plato; // Asigna el objeto encontrado a la propiedad Plato
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            // Verifica si el id es nulo o si el contexto de platos es nulo.
            if (id == null || _context.Platos == null)
            {
                // Si alguna de las condiciones anteriores es verdadera, se devuelve un error 404 (Not Found).
                return NotFound();
            }
            // Busca el Plato con el id proporcionado de forma asíncrona.
            var platos = await _context.Platos.FindAsync(id);
            // Si el Plato no se encuentra, se devuelve un error 404.
            if (platos == null)
            {
                return NotFound();
            }
            // Si se encuentra el Plato, se procede a eliminarlo.
            Plato = platos; // Asigna el Plato encontrado a la propiedad Plato.
            _context.Platos.Remove(Plato); // Elimina el Plato del contexto.
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos de forma asíncrona.
            // Redirige a la página de índice después de eliminar el Plato.
            return RedirectToPage("./Index");
        }
    }
}
