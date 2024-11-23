using RestoStockDB1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestoStockDB1.Models;
namespace RestoStockDB1.Pages.Ingredientes
{
    public class DeleteModel : PageModel
    {
        private readonly RestoStockContext _context;

        public DeleteModel(RestoStockContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Ingrediente Ingrediente { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Ingredientes == null)
            {
                return NotFound();
            }

            var ingrediente = await _context.Ingredientes
                .Include(i => i.DetallesPlatos) // Incluir relaciones si es necesario
                .FirstOrDefaultAsync(m => m.IngredienteId == id);

            if (ingrediente == null)
            {
                return NotFound();
            }
            else
            {
                Ingrediente = ingrediente;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Ingredientes == null)
            {
                return NotFound();
            }

            var ingrediente = await _context.Ingredientes
                .Include(i => i.DetallesPlatos) // Incluye los detalles para manejar relaciones
                .FirstOrDefaultAsync(m => m.IngredienteId == id);

            if (ingrediente != null)
            {
                Ingrediente = ingrediente;

                // Eliminar las relaciones muchos-a-muchos antes de eliminar el ingrediente
                if (Ingrediente.DetallesPlatos != null && Ingrediente.DetallesPlatos.Any())
                {
                    _context.DetallesPlatos.RemoveRange(Ingrediente.DetallesPlatos);
                }

                _context.Ingredientes.Remove(Ingrediente);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
