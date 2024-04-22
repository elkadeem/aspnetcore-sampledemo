using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationSample.Web.Models;

namespace ApplicationSample.Web.Pages.Departments
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationSample.Web.Models.ApplicationDbContext _context;

        public IndexModel(ApplicationSample.Web.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Deparment> Deparment { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Deparment = await _context.Departments.ToListAsync();
        }
    }
}
