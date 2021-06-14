using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Data;
using OnlineBankingApp.Models;

namespace OnlineBankingApp.Pages.FundTransfers
{
    public class DeleteModel : PageModel
    {
        private readonly OnlineBankingApp.Data.OnlineBankingAppContext _context;

        public DeleteModel(OnlineBankingApp.Data.OnlineBankingAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FundTransfer FundTransfer { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FundTransfer = await _context.FundTransfer.FirstOrDefaultAsync(m => m.ID == id);

            if (FundTransfer == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FundTransfer = await _context.FundTransfer.FindAsync(id);

            if (FundTransfer != null)
            {
                _context.FundTransfer.Remove(FundTransfer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
