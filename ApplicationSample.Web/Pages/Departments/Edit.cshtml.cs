using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApplicationSample.Web.Models;

namespace ApplicationSample.Web.Pages.Departments
{
    public class EditModel : PageModel
    {
        private readonly ApplicationSample.Web.Models.ApplicationDbContext _context;

        public EditModel(ApplicationSample.Web.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Deparment Deparment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deparment =  await _context.Departments.FirstOrDefaultAsync(m => m.Id == id);
            if (deparment == null)
            {
                return NotFound();
            }
            Deparment = deparment;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrWhiteSpace(Deparment.Name)
                 && _context.Departments.Any(c => c.Id != Deparment.Id && c.Name == Deparment.Name))
            {
                ModelState.AddModelError("Deparment.Name", "Name already exists");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Deparment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeparmentExists(Deparment.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DeparmentExists(int id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }
    }
}
