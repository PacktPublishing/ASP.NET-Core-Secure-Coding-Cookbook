using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBankingApp.Models;

namespace OnlineBankingApp.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;

        public IndexModel(
            UserManager<Customer> userManager,
            SignInManager<Customer> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

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

            [DisplayFormat(DataFormatString = "{mm/dd/yyyy}")]
            [DataType(DataType.Date)]
            public DateTime DateOfBirth { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(Customer user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if (Input.FirstName != user.FirstName)
            {
                user.FirstName = Input.FirstName;
            }            

            if (Input.MiddleName != user.MiddleName)
            {
                user.MiddleName = Input.MiddleName;
            }            

            if (Input.LastName != user.LastName)
            {
                user.LastName = Input.LastName;
            }            

            if (Input.DateOfBirth != user.DateOfBirth)
            {
                user.DateOfBirth = Input.DateOfBirth;
            }            

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
