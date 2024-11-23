using RestoStockDB1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestoStockDB1.Models;

namespace RestoStockDB1.Pages.Proovedores
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

            var proovedor = await _context.Proovedores.FirstOrDefaultAsync(m => m.ProovedorId == id);
            if (proovedor == null)
            {
                return NotFound();
            }

            Proovedor = proovedor; // Asigna el objeto encontrado a la propiedad Proovedor
            return Page();
        }



        public async Task<IActionResult> OnPostAsync(int? id)
        {
            // Verifica si el id es nulo o si el contexto de proveedores es nulo.
            if (id == null || _context.Proovedores == null)
            {
                // Si alguna de las condiciones anteriores es verdadera, se devuelve un error 404 (Not Found).
                return NotFound();
            }
            // Busca el Proveedor con el id proporcionado de forma asíncrona.
            var proovedores = await _context.Proovedores.FindAsync(id);
            // Si el Proveedor no se encuentra, se devuelve un error 404.
            if (proovedores == null)
            {
                return NotFound();
            }
            // Si se encuentra el Proveedor, se procede a eliminarlo.
            Proovedor = proovedores; // Asigna el Proveedor encontrado a la propiedad Proveedores.
            _context.Proovedores.Remove(Proovedor); // Elimina el Proveedor del contexto.
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos de forma asíncrona.
            // Redirige a la página de índice después de eliminar el Proveedor.
            return RedirectToPage("./Index");
        }
    }
}