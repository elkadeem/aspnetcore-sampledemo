using ApplicationSample.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ApplicationSample.Web.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationSample.Web.Models.ApplicationDbContext _context;

        public DeleteModel(ApplicationSample.Web.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Select(c => new Customer
                {
                    Address = c.Address,
                    BirthDay = c.BirthDay,
                    Email = c.Email,
                    Id = c.Id,
                    IsActive = c.IsActive,
                    Name = c.Name,
                    Phone = c.Phone
                }).FirstOrDefaultAsync(m => m.Id == id);

            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                Customer = customer;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null && id <= 0)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                Customer = customer;
                _context.Customers.Remove(Customer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
