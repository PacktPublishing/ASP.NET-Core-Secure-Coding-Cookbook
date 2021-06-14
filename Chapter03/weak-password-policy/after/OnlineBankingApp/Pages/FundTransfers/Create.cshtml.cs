using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineBankingApp.Data;
using OnlineBankingApp.Models;

namespace OnlineBankingApp.Pages.FundTransfers
{
    public class CreateModel : AccountPageModel
    {
        private readonly OnlineBankingApp.Data.OnlineBankingAppContext _context;

        public CreateModel(OnlineBankingApp.Data.OnlineBankingAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateAccountsDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public FundTransfer FundTransfer { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyFundTransfer = new FundTransfer();

            if (await TryUpdateModelAsync<FundTransfer>(
                 emptyFundTransfer,
                 "fundtransfer",  
                 f => f.ID, f => f.AccountFrom, f => f.AccountTo, f => f.Amount, f=> f.TransactionDate, f => f.Note))
            {
                _context.FundTransfer.Add(emptyFundTransfer);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateAccountsDropDownList(_context);
            return Page();

        }
    }
}
