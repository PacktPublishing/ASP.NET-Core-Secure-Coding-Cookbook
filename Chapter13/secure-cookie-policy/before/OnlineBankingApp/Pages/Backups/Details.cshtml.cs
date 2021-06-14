using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Data;
using OnlineBankingApp.Models;

namespace OnlineBankingApp.Pages.Backups
{
    public class DetailsModel : PageModel
    {
        private readonly OnlineBankingApp.Data.OnlineBankingAppContext _context;

        public DetailsModel(OnlineBankingApp.Data.OnlineBankingAppContext context)
        {
            _context = context;
        }

        public Backup Backup { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Backup = await _context.Backup.FirstOrDefaultAsync(m => m.ID == id);

            if (Backup == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
