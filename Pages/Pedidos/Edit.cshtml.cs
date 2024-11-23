using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestoStockDB1.Data;
using RestoStockDB1.Models;

namespace RestoStockDB1.Pages.Pedidos
{
    public class EditModel : PageModel
    {
        private readonly RestoStockContext _context;

        public EditModel(RestoStockContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pedido Pedido { get; set; } = default!;

        // Propiedad para almacenar la lista de proveedores
        public SelectList ProovedoresSelectList { get; set; } = default!;

        // Cargar los proveedores para el select
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Obtener el pedido junto con su proveedor
            var pedido = await _context.Pedidos
                .Include(p => p.Proovedor)
                .FirstOrDefaultAsync(p => p.PedidoId == id);

            // Si no se encuentra el pedido, devolver un error 404
            if (pedido == null)
            {
                return NotFound();
            }

            // Cargar los proveedores para el dropdown de selección
            ProovedoresSelectList = new SelectList(await _context.Proovedores.ToListAsync(), "ProovedorId", "NombreEmpresa");

            // Asignar el pedido al modelo de la vista
            Pedido = pedido;
            return Page();
        }

        // Guardar los cambios del pedido
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Recargar los proveedores para el dropdown si el modelo no es válido
                ProovedoresSelectList = new SelectList(await _context.Proovedores.ToListAsync(), "ProovedorId", "NombreEmpresa");
                return Page();
            }

            _context.Attach(Pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(Pedido.PedidoId))
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

        // Verificar si el pedido existe
        private bool PedidoExists(int id)
        {
            return (_context.Pedidos?.Any(e => e.PedidoId == id)).GetValueOrDefault();
        }
    }
}
