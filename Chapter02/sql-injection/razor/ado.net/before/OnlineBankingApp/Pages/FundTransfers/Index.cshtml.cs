using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBankingApp.Data;
using OnlineBankingApp.Models;

using Microsoft.Extensions.Configuration;

namespace OnlineBankingApp.Pages.FundTransfers
{
    public class IndexModel : PageModel
    {

        public IList<FundTransfer> FundTransfer { get;set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        private readonly FundTransferDAL fundtransferDAL;  

        public IndexModel(IConfiguration configuration)
        {
            fundtransferDAL = new FundTransferDAL(configuration);
        }

        public async Task OnGetAsync()
        {
            
            if (!string.IsNullOrEmpty(SearchString))
            {
                FundTransfer = await Task.FromResult(fundtransferDAL.GetFundTransfers(SearchString).ToList());
            }
            else {
                FundTransfer = await Task.FromResult(fundtransferDAL.GetFundTransfers().ToList()); 
            }

        }
    }
}
