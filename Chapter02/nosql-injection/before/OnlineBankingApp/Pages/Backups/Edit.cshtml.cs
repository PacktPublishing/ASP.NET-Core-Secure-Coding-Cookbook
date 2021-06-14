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

namespace OnlineBankingApp.Pages.Backups
{
    public class EditModel : PageModel
    {
        private readonly OnlineBankingApp.Data.OnlineBankingAppContext _context;

        public EditModel(OnlineBankingApp.Data.OnlineBankingAppContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Backup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BackupExists(Backup.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BackupExists(int id)
        {
            return _context.Backup.Any(e => e.ID == id);
        }
    }
}
