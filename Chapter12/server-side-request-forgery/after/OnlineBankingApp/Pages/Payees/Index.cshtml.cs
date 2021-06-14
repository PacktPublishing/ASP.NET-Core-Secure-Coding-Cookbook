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
using System.Text.RegularExpressions;
using System.Net.Http;

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
            
            if (string.IsNullOrWhiteSpace(mongouri))
            {
                mongouri = "http://localhost:28017/test/Payees/";
            }
            else 
            {
                if(!IsValidMongoRestUri(mongouri))
                {
                    throw new HttpRequestException("Invalid Request");
                }
            }
            Roots = await _payeeService.GetPayeesAsync(mongouri);
        }

        private bool IsValidMongoRestUri(string mongouri)
        {
            string pattern = @"^http://localhost:28017/test/Payees/\\?$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(mongouri);
        }


    }
}
