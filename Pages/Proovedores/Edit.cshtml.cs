using RestoStockDB1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestoStockDB1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestoStockDB1.Pages.Proovedores
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
            var proovedor = await _context.Proovedores.FirstOrDefaultAsync(m => m.ProovedorId == id);
            if (proovedor == null)
            {
                return NotFound();
            }
            else
            {
                Proovedor = proovedor;
                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                //return Page(); // Si el modelo no es v�lido, vuelve a la misma p�gina
            }
            _context.Attach(Proovedor).State = EntityState.Modified; // Marca la entidad como modificada
            try
            {
                await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
            }
            catch (DbUpdateConcurrencyException) // Si ocurre un error de concurrencia
            {
                if (!ProveedoresExists(Proovedor.ProovedorId)) // Si el proveedor ya no existe
                {
                    return NotFound(); // Devuelve un error 404
                }
                else
                {
                    throw; // Re lanza la excepci�n para que sea manejada por niveles superiores
                }
            }
            return RedirectToPage("./Index"); // Redirige a la p�gina de �ndice
        }
        private bool ProveedoresExists(int id)
        {
            return (_context.Proovedores?.Any(e => e.ProovedorId == id)).GetValueOrDefault();
        }
    }
}