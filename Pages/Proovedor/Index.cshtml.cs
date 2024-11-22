using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestoStockDB1.Data;
using RestoStockDB1.Models;

namespace RestoStockDB.Pages.Proovedores
{
    //[Authorize]
    public class IndexModel : PageModel
    {
        private readonly RestoStockContext _context;

        public IndexModel(RestoStockContext context)
        {
            _context = context;
        }

        public IList<Proovedor> Proovedores { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Proovedores != null)
            {
                Proovedores = await _context.Proovedores.ToListAsync();
            }
        }
    }
}

