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
    public class AccountPageModel : PageModel
    {
        public SelectList AccountSL { get; set; }

        public void PopulateAccountsDropDownList(OnlineBankingAppContext _context,
            object selectedAccount = null)
        {
            ViewData["CustomerID"] = 1;
            var accountsQuery = from a in _context.Account
                                   where a.CustomerID == (int) ViewData["CustomerID"]
                                   orderby a.ID
                                   select a;

            AccountSL = new SelectList(accountsQuery.AsNoTracking(),
                        "ID", "Name", selectedAccount);
        }
    }
}