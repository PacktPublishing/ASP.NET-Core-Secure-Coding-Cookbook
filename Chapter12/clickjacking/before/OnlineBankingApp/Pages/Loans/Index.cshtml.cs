using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Data;
using OnlineBankingApp.Models;

namespace OnlineBankingApp.Pages.Loans
{
    public class IndexModel : PageModel
    {
        private readonly OnlineBankingApp.Data.OnlineBankingAppContext _context;

        public IndexModel(OnlineBankingApp.Data.OnlineBankingAppContext context)
        {
            _context = context;
        }

        public IList<Loan> Loan { get;set; }
        
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }


        public async Task OnGetAsync()
        {
            var loans = from l in _context.Loan
                        select l;
            if (!string.IsNullOrEmpty(SearchString))
            {
                loans = loans.Where(l => l.Note.Contains(SearchString));
            }

            Loan = await loans.ToListAsync();
        }
    }
}
