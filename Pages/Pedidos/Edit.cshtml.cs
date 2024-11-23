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

        // Propiedad BindProperty para vincular el Pedido a la vista
        [BindProperty]
        public Pedido Pedido { get; set; } = default!;

        // M�todo GET para cargar los datos de un Pedido espec�fico
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Verifica que el id no sea nulo y que la tabla Pedidos no est� vac�a
            if (id == 0 || _context.Pedidos == null)
            {
                return NotFound(); // Si no existe el id, retorna error 404
            }

            // Obtiene el pedido de la base de datos, incluyendo la relaci�n con el proveedor
            var pedido = await _context.Pedidos
                .Include(p => p.Proovedor)
                .FirstOrDefaultAsync(m => m.PedidoId == id);

            // Si no se encuentra el pedido, retorna error 404
            if (pedido == null)
            {
                return NotFound();
            }

            // Asigna el pedido encontrado a la propiedad BindProperty
            Pedido = pedido;

            // Carga las listas desplegables de Proveedores para la vista
            ViewData["Proveedores"] = new SelectList(await _context.Proovedores.ToListAsync(), "ProovedorId", "Nombre");

            return Page(); // Retorna la p�gina con los datos cargados
        }

        // M�todo POST para guardar los cambios realizados en el pedido
        public async Task<IActionResult> OnPostAsync()
        {
            // Verifica si el modelo es v�lido
            if (!ModelState.IsValid)
            {
                // Si el modelo no es v�lido, recarga las listas desplegables de Proveedores
                ViewData["Proveedores"] = new SelectList(await _context.Proovedores.ToListAsync(), "ProovedorId", "Nombre");
                return Page(); // Retorna la p�gina con los datos sin cambios
            }

            // Marca el Pedido como modificado para que se guarde en la base de datos
            _context.Attach(Pedido).State = EntityState.Modified;

            try
            {
                // Intenta guardar los cambios realizados
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // En caso de error por concurrencia
                if (!PedidoExists(Pedido.PedidoId))
                {
                    return NotFound(); // Si no existe el pedido, retorna error 404
                }
                else
                {
                    throw; // Si no es un error de concurrencia, lanza la excepci�n
                }
            }

            // Si la edici�n fue exitosa, redirige al listado de pedidos
            return RedirectToPage("./Index");
        }

        // M�todo privado para verificar si el Pedido existe en la base de datos
        private bool PedidoExists(int id)
        {
            return (_context.Pedidos?.Any(e => e.PedidoId == id)).GetValueOrDefault();
        }
    }
}
