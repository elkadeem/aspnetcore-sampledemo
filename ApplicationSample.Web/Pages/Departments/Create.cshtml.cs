using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationSample.Web.Models;

namespace ApplicationSample.Web.Pages.Departments
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationSample.Web.Models.ApplicationDbContext _context;

        public CreateModel(ApplicationSample.Web.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Deparment Deparment { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrWhiteSpace(Deparment.Name)
                 && _context.Departments.Any(c => c.Name == Deparment.Name))
            {
                ModelState.AddModelError("Deparment.Name", "Name already exists");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Departments.Add(Deparment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
