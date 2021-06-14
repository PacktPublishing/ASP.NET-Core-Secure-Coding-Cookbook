using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Data;
using OnlineBankingApp.Models;

using System.Security.Claims;

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

            var loggedInUser = HttpContext.User;
            var customerId = loggedInUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            if (id == null)
            {
                return NotFound();
            }

            fundTransfer = await _context.FundTransfer
                                .OrderBy(f => f.TransactionDate)
                                .FirstOrDefaultAsync<FundTransfer>(f => f.CustomerID == customerId);
                                //.Include(f => f.Customer)
                                //.Where(f => f.Customer.Id == "0976cf3d-e934-494e-8a22-d7882deee2f1")                         
                                //.FirstOrDefaultAsync<FundTransfer>();

            //var customerid = fundTransfer.Where(f => f.Customer.Id == "0976cf3d-e934-494e-8a22-d7882deee2f1").Single().Id;

            if(fundTransfer == null)
            {
                return Unauthorized();
            }            

            // if (fundTransfer == null)
            // {
            //     return NotFound();
            // }
            return Page();
        }
    }
}
