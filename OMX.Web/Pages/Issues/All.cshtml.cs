using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OMX.Data;
using OMX.Models;

namespace OMX.Web.Pages.Issues
{
    [Authorize(Roles = "Administrator")]
    public class AllModel : PageModel
    {
        private readonly OMX.Data.OmxDbContext _context;

        public AllModel(OMX.Data.OmxDbContext context)
        {
            _context = context;
        }

        public IList<Report> Report { get;set; }

        public async Task OnGetAsync()
        {
            Report = await _context.Reports
                .Include(r => r.User).ToListAsync();
        }

        public async Task OnPostResolveAsync(int id)
        {
            var report = await  _context.Reports.FirstOrDefaultAsync(e => e.Id == id);
            report.IsResolved = true;
            Report = await _context.Reports
                .Include(r => r.User).ToListAsync();
            await this._context.SaveChangesAsync();
        }
    }
}
