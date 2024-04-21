using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ApplicationSample.Web.Pages.Customers
{
    public class CustomerPhotoModel : PageModel
    {
        private readonly ApplicationSample.Web.Models.ApplicationDbContext _context;

        public CustomerPhotoModel(ApplicationSample.Web.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null && id <= 0)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Select(c => new 
                {
                    Id = c.Id,
                    IdPhoto = c.IdPhoto,
                    IdPhotoContentType = c.IdPhotoContentType
                })
                .FirstOrDefaultAsync(m => m.Id == id);

            if (customer == null 
                || customer.IdPhoto == null
                || string.IsNullOrWhiteSpace(customer.IdPhotoContentType))
            {
                return NotFound();
            }


            return File(customer.IdPhoto, customer.IdPhotoContentType);
        }
    }
}
