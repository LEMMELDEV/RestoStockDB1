using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestoStockDB1.Data;
using RestoStockDB1.Models;

namespace RestoStockDB1.Pages.DetallesPlatos
{
    public class CreateModel : PageModel
    {
        private readonly RestoStockContext _context;

        public CreateModel(RestoStockContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DetallePlato DetallePlato { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.DetallesPlatos == null || DetallePlato == null)
            {
                return Page();
            }

            _context.DetallesPlatos.Add(DetallePlato);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
