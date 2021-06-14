using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading.Tasks;

using OnlineBankingApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;

using System.Runtime.Serialization.Formatters.Binary;

namespace OnlineBankingApp.Pages.Loans
{
    public class UploadModel : PageModel
    {
        private IWebHostEnvironment _environment;
        private readonly OnlineBankingApp.Data.OnlineBankingAppContext _context;

        public UploadModel(IWebHostEnvironment environment, OnlineBankingApp.Data.OnlineBankingAppContext context)
        {
            _environment = environment;
            _context = context;
        }

        [BindProperty]
        public IFormFile Upload { get; set; }
        
        public async Task OnPostAsync()
        {
            Loan emptyLoan = null;
            var file = Path.Combine(_environment.ContentRootPath, "uploads", Upload.FileName);

            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Binder = new LoanDeserializationBinder();
                fileStream.Position = 0;
                emptyLoan = (Loan) formatter.Deserialize(fileStream);
            }

            var loggedInUser = HttpContext.User;
            var customerId = loggedInUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            emptyLoan.CustomerID = customerId;
            emptyLoan.TransactionDate = DateTime.Now;
            emptyLoan.Status = LoanStatus.Pending;

            if (await TryUpdateModelAsync<Loan>(
                emptyLoan,
                "loan",  
                l => l.ID, l => l.CustomerID, l => l.Amount, l => l.PeriodInMonths, l => l.TransactionDate, l => l.Note))
            {
                _context.Loan.Add(emptyLoan);
                await _context.SaveChangesAsync();
            }

        }
    }
}