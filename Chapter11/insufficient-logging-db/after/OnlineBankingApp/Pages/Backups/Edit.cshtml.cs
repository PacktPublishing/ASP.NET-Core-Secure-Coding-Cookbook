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
using Microsoft.Extensions.Logging;

namespace OnlineBankingApp.Pages.Backups
{
    public class EditModel : PageModel
    {
        private readonly OnlineBankingApp.Data.OnlineBankingAppContext _context;
        private readonly ILogger<EditModel> _logger;

        public EditModel(OnlineBankingApp.Data.OnlineBankingAppContext context, 
                        ILogger<EditModel> logger)
        {
            _logger = logger;
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

            try{
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex){
                if (!BackupExists(Backup.ID)){
                    _logger.LogError("Backup not found");
                    return NotFound();
                }
                else{
                    _logger.LogError($"An error occurred in backing up the DB { ex.Message } ");
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
