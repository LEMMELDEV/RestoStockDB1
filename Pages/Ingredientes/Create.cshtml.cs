using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestoStockDB1.Data; 
using RestoStockDB1.Models;

namespace RestoStockDB1.Pages.Ingredientes
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
        public Ingrediente Ingrediente { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Ingredientes == null || Ingrediente == null)
            {
                return Page();
            }

            _context.Ingredientes.Add(Ingrediente);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
