using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Data;
using OnlineBankingApp.Models;

namespace OnlineBankingApp.Pages.FundTransfers
{
    public class EditModel : PageModel
    {
        private readonly OnlineBankingApp.Data.OnlineBankingAppContext _context;

        public EditModel(OnlineBankingApp.Data.OnlineBankingAppContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(FundTransfer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FundTransferExists(FundTransfer.ID))
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

        private bool FundTransferExists(Guid id)
        {
            return _context.FundTransfer.Any(e => e.ID == id);
        }
    }
}
