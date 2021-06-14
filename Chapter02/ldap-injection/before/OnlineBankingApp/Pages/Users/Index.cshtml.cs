using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Data;
using OnlineBankingApp.Models;
using OnlineBankingApp.Services;

namespace OnlineBankingApp.Pages.Users
{
    public class IndexModel : PageModel
    {
        public User LdapUser { get;set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        private readonly ILdapDirectoryService _ldapDirectoryService;

        public IndexModel(ILdapDirectoryService ldapDirectoryService)
        {
            _ldapDirectoryService = ldapDirectoryService;
        }

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                LdapUser = _ldapDirectoryService.Search(SearchString);
            }
        }
    }
}
