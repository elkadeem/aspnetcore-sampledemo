using ApplicationSample.Web.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ApplicationSample.Web.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationSample.Web.Models.ApplicationDbContext _context;

        public IndexModel(ApplicationSample.Web.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CustomerDto> Customer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Customer = await _context.Customers
                .Select(c => new CustomerDto {
                  Address = c.Address,
                  BirthDay = c.BirthDay,
                  Email = c.Email,
                  Id = c.Id,
                  IsActive = c.IsActive,
                  Name = c.Name,
                  Phone = c.Phone                  
                 }).ToListAsync();
        }
    }
}
