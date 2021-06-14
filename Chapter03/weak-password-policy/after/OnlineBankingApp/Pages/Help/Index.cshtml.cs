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

namespace OnlineBankingApp.Pages.Knowledgebase
{
    public class IndexModel : PageModel
    {
        public IList<Knowledge> Knowledge { get;set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        private readonly IKnowledgebaseService _knowledgebaseService;

        public IndexModel(IKnowledgebaseService knowledgebaseService)
        {
            _knowledgebaseService = knowledgebaseService;
        }

        public void OnGet()
        {
            Knowledge = new List<Knowledge>();
            
            if (!string.IsNullOrEmpty(SearchString))
            {
                Knowledge = _knowledgebaseService.Search(SearchString);
            }
        }
    }
}
