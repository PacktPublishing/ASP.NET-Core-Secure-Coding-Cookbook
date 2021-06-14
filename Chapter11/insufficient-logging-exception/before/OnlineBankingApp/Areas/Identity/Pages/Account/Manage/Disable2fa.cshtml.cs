using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OnlineBankingApp.Models;

namespace OnlineBankingApp.Areas.Identity.Pages.Account.Manage
{
    public class Disable2faModel : PageModel
    {
        private readonly UserManager<Customer> _customerManager;
        private readonly ILogger<Disable2faModel> _logger;

        public Disable2faModel(
            UserManager<Customer> userManager,
            ILogger<Disable2faModel> logger)
        {
            _customerManager = userManager;
            _logger = logger;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var customer = await _customerManager.GetUserAsync(User);
            if (customer == null)
            {
                return NotFound($"Unable to load customer with ID '{_customerManager.GetUserId(User)}'.");
            }

            if (!await _customerManager.GetTwoFactorEnabledAsync(customer))
            {
                throw new InvalidOperationException($"Cannot disable 2FA for customer with ID '{_customerManager.GetUserId(User)}' as it's not currently enabled.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var customer = await _customerManager.GetUserAsync(User);
            if (customer == null)
            {
                return NotFound($"Unable to load customer with ID '{_customerManager.GetUserId(User)}'.");
            }

            var disable2faResult = await _customerManager.SetTwoFactorEnabledAsync(customer, false);
            if (!disable2faResult.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred disabling 2FA for customer with ID '{_customerManager.GetUserId(User)}'.");
            }

            _logger.LogInformation("Customer with ID '{UserId}' has disabled 2fa.", _customerManager.GetUserId(User));
            StatusMessage = "2fa has been disabled. You can reenable 2fa when you setup an authenticator app";
            return RedirectToPage("./TwoFactorAuthentication");
        }
    }
}