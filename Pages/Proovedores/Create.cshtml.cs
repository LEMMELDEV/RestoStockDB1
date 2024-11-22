using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestoStockDB1.Data;
using RestoStockDB1.Models;

namespace RestoStockDB1.Pages.Proovedores
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
        public Proovedor Proovedor { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Proovedores == null || Proovedor == null)
            {
                return Page();
            }

            _context.Proovedores.Add(Proovedor);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}