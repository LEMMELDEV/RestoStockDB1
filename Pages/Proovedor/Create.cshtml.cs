using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestoStockDB1.Data;
using RestoStockDB1.Models;

namespace Proovedores
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
        public Proovedor Proovedores
        { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Proovedores == null || Proovedores == null)
            {
                // return Page();
            }

            _context.Proovedores.Add(Proovedores);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
