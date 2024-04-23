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
    public class DetailsModel : PageModel
    {
        private readonly ApplicationSample.Web.Models.ApplicationDbContext _context;

        public DetailsModel(ApplicationSample.Web.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public Deparment Deparment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deparment = await _context.Departments.FirstOrDefaultAsync(m => m.Id == id);
            if (deparment == null)
            {
                return NotFound();
            }
            else
            {
                Deparment = deparment;
            }
            return Page();
        }
    }
}
