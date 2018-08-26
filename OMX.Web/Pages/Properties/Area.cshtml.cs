using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OMX.Data;
using OMX.Models;

namespace OMX.Web.Pages.Properties
{
    public class AreaModel : PageModel
    {
        private readonly OMX.Data.OmxDbContext _context;

        public AreaModel(OMX.Data.OmxDbContext context)
        {
            _context = context;
        }

        public IList<Property> Property { get;set; }

        public async Task OnGetAsync(int id)
        {
            Property = await _context.Properties
                .Include(e => e.Address)
                .Include(e => e.User)
                .Include(e=> e.ImageNames)
                .Where(e => e.AddressId == id)
                .ToListAsync();
        }
    }
}
