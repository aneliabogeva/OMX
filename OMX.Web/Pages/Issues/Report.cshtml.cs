using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OMX.Data;
using OMX.Models;

namespace OMX.Web.Pages.Issues
{
    [Authorize]
    public class ReportModel : PageModel
    {
        private readonly OmxDbContext _context;
        private readonly UserManager<User> userManager;

        public ReportModel(OmxDbContext context, UserManager<User> userManager)
        {
            _context = context;
            this.userManager = userManager;            
        }

        public IActionResult OnGet()
        {
        
            return Page();
        }

        [BindProperty]
        public Report Report { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var userId = this.userManager.GetUserId(HttpContext.User);
            this.Report.UserId = userId;
            _context.Reports.Add(Report);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}