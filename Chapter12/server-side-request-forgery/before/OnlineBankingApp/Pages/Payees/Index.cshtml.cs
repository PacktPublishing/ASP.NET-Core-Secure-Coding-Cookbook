using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Data;
using OnlineBankingApp.Models;
using OnlineBankingApp.Services;

namespace OnlineBankingApp.Pages.Payees
{
    public class IndexModel : PageModel
    {
        private readonly IPayeeService _payeeService;

        public IndexModel(IPayeeService payeeService)
        {
            _payeeService = payeeService;
        }

        public Root Roots { get;set; }

        public async Task OnGetAsync()
        {
            var mongouri = Request.Query["mongouri"];
            
            mongouri = string.IsNullOrWhiteSpace(mongouri) 
                    ? "http://localhost:28017/test/Payees/" 
                    : Request.Query["mongouri"];

            Roots = await _payeeService.GetPayeesAsync(mongouri);
        }
    }
}
