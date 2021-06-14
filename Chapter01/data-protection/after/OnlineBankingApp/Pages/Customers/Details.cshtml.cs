using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Data;
using OnlineBankingApp.Models;
using Microsoft.AspNetCore.DataProtection;

namespace OnlineBankingApp.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly OnlineBankingApp.Data.OnlineBankingAppContext _context;
        private readonly IDataProtector _dataProtector;

        public DetailsModel(OnlineBankingApp.Data.OnlineBankingAppContext context, IDataProtectionProvider dataProtector)
        {
            _context = context;
            _dataProtector = dataProtector.CreateProtector("OnlineBankingApp.Pages.Customers");;
        }

        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var decID = Int32.Parse(_dataProtector.Unprotect(id));

            Customer = await _context.Customer.FirstOrDefaultAsync(m => m.ID == decID);

            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
