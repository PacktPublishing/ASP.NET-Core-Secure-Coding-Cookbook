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
    public class DetailsModel : PageModel
    {
        private readonly OnlineBankingApp.Data.OnlineBankingAppContext _context;

        public DetailsModel(OnlineBankingApp.Data.OnlineBankingAppContext context)
        {
            _context = context;
        }

        public FundTransfer fundTransfer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            fundTransfer = await _context.FundTransfer
                                .Where(f => f.ID == id)
                                .Include(f => f.Customer)
                                .OrderBy(f => f.TransactionDate)
                                .FirstOrDefaultAsync<FundTransfer>();

            if (fundTransfer == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
