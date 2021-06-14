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
    public class IndexModel : PageModel
    {
        private readonly OnlineBankingApp.Data.OnlineBankingAppContext _context;

        public IndexModel(OnlineBankingApp.Data.OnlineBankingAppContext context)
        {
            _context = context;
        }

        public IList<FundTransfer> FundTransfer { get;set; }

        public async Task OnGetAsync()
        {
            FundTransfer = await _context.FundTransfer.ToListAsync();
        }
    }
}
