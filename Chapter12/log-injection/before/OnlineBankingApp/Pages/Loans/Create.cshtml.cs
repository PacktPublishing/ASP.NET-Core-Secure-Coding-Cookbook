using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineBankingApp.Data;
using OnlineBankingApp.Models;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace OnlineBankingApp.Pages.Loans
{
    public class CreateModel : PageModel
    {
        private readonly OnlineBankingApp.Data.OnlineBankingAppContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(OnlineBankingApp.Data.OnlineBankingAppContext context, 
                ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Loan Loan { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var loggedInUser = HttpContext.User;
            var customerId = loggedInUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var emptyLoan = new Loan {
                CustomerID = customerId,
                TransactionDate = DateTime.Now
            };

            if (await TryUpdateModelAsync<Loan>(
                 emptyLoan,
                 "loan",  
                 l => l.ID, l => l.CustomerID, l => l.Amount, l => l.PeriodInMonths, l => l.TransactionDate, l => l.Note))
            {
                _context.Loan.Add(emptyLoan);
                await _context.SaveChangesAsync();
            }
            else {
                _logger.LogWarning("Problem creating loan:" + emptyLoan.CustomerID + ";" 
                            + emptyLoan.Amount + ";" 
                            + emptyLoan.PeriodInMonths + ";" 
                            + emptyLoan.Note);
            }

            return RedirectToPage("./Index");
        }
    }
}
