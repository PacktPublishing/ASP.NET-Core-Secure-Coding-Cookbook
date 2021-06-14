using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Data;
using OnlineBankingApp.Models;

using Microsoft.AspNetCore.Authorization;

namespace OnlineBankingApp.Pages.FundTransfers
{
    [Authorize(Roles = "Customer,ActiveCustomer")]
    public class IndexModel : PageModel
    {
        private readonly OnlineBankingApp.Data.OnlineBankingAppContext _context;

        public IndexModel(OnlineBankingApp.Data.OnlineBankingAppContext context)
        {
            _context = context;
        }

        public IList<FundTransfer> FundTransfer { get;set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var fundtransfer = from f in _context.FundTransfer
                 select f;

            if (!string.IsNullOrEmpty(SearchString))
            {
                fundtransfer = _context.FundTransfer.FromSqlInterpolated($"Select * from FundTransfer Where Note Like {"%" + SearchString + "%"}");
                
                //This line is vulnerable to SQL injection attacks
                //fundtransfer = _context.FundTransfer.FromSqlRaw($"Select * from FundTransfer Where Note Like '%{SearchString}%'");
            }

            FundTransfer = await fundtransfer.ToListAsync();
        }
    }
}
