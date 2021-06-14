using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using OnlineBankingApp.Data;
using OnlineBankingApp.Models;
using OnlineBankingApp.Services;

namespace OnlineBankingApp.Pages.Backups
{
    public class CreateModel : PageModel
    {
        private readonly OnlineBankingApp.Data.OnlineBankingAppContext _context;

        public CreateModel(OnlineBankingApp.Data.OnlineBankingAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Backup Backup { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyBackup = new Backup();

            if (await TryUpdateModelAsync<Backup>(
                emptyBackup,
                "backup",
                b => b.Name, b => b.BackupDate))
            {
                emptyBackup.BackupDate = DateTime.Now;
                _context.Backup.Add(emptyBackup);
                await _context.SaveChangesAsync();

                var service = new BackupService();
                await service.BackupDB(emptyBackup.Name);

                return RedirectToPage("./Index");
            }

            return Page();

        }
    }
}
