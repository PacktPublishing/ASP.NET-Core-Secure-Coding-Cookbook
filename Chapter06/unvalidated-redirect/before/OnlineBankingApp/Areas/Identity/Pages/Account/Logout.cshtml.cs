using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OnlineBankingApp.Models;

namespace OnlineBankingApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<Customer> _signInManager;
        private readonly ILogger<LogoutModel> _log;

        public LogoutModel(SignInManager<Customer> signInManager, ILogger<LogoutModel> log)
        {
            _signInManager = signInManager;
            _log = log;
        }

        public async Task<IActionResult> OnGet(string url = null)
        {
            await _signInManager.SignOutAsync();
            _log.LogInformation("User logged out.");
            if (url != null)
            {
                return Redirect(url);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
