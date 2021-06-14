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
    public class IndexModel : PageModel
    {
        private readonly OnlineBankingApp.Data.OnlineBankingAppContext _context;
        private readonly IDataProtector _dataProtector;

        public IndexModel(OnlineBankingApp.Data.OnlineBankingAppContext context, IDataProtectionProvider dataProtector)
        {
            _context = context;
            _dataProtector = dataProtector.CreateProtector("OnlineBankingApp.Pages.Customers");;
        }

        public IList<Customer> Customer { get;set; }

        public async Task OnGetAsync()
        {
            foreach (var cust in _context.Customer)
            {
                cust.EncCustomerID = _dataProtector.Protect(cust.ID.ToString());
            }

            Customer = await _context.Customer.ToListAsync();
        }

    }
}
