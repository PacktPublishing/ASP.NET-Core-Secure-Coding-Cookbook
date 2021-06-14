using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using OnlineBankingApp.Models;
using OnlineBankingApp.Services;

namespace OnlineBankingApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Customer> _signInManager;
        private readonly UserManager<Customer> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ICryptoService _cryptoService;
        private readonly RoleManager<IdentityRole> _roleManager;          
        private readonly IPasswordHasher<Customer> _passwordHasher;

        public RegisterModel(
            UserManager<Customer> userManager,
            SignInManager<Customer> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ICryptoService cryptoService,
            RoleManager<IdentityRole> roleManager,
            IPasswordHasher<Customer> passwordHasher)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _cryptoService = cryptoService;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
            [Display(Name = "First Name")]
            [StringLength(60, MinimumLength = 3)]
            [Required]        
            public string FirstName { get; set; }

            [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
            [Display(Name = "Middle Name")]
            [StringLength(60, MinimumLength = 3)]
            [Required]        
            public string MiddleName { get; set; }

            [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
            [Display(Name = "Last Name")]
            [StringLength(60, MinimumLength = 3)]
            [Required]        
            public string LastName { get; set; }

            [Required]
            [DisplayFormat(DataFormatString = "{mm/dd/yyyy}")]
            [DataType(DataType.Date)]
            public DateTime DateOfBirth { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new Customer { 
                    FirstName = Input.FirstName,
                    MiddleName = Input.MiddleName,
                    LastName = Input.LastName,
                    DateOfBirth = Input.DateOfBirth,                    
                    UserName = Input.Email, 
                    Email = Input.Email 
                };

                _userManager.PasswordHasher = _passwordHasher;

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(Role.Customer))   
                    {  
                        await _roleManager.CreateAsync(new IdentityRole(Role.Customer));  
                    }  
  
                    if (!await _roleManager.RoleExistsAsync(Role.ActiveCustomer))  
                    {  
                        await _roleManager.CreateAsync(new IdentityRole(Role.ActiveCustomer));  
                    }

                    if (!await _roleManager.RoleExistsAsync(Role.PendingCustomer))  
                    {  
                        await _roleManager.CreateAsync(new IdentityRole(Role.PendingCustomer));  
                    }

                    if (!await _roleManager.RoleExistsAsync(Role.SuspendedCustomer))  
                    {  
                        await _roleManager.CreateAsync(new IdentityRole(Role.SuspendedCustomer));  
                    }

                    await _userManager.AddToRoleAsync(user,Role.Customer);
                    await _userManager.AddToRoleAsync(user,Role.PendingCustomer);  

                    _logger.LogInformation("User created a new account with password.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
