using ApplicationSample.Web.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        public IList<CustomerDto> Customers { get;set; } = default!;
        public int PageSize { get; set; } = 3;
        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalItemsCount { get; set; }
        public async Task OnGetAsync()
        {
            PageNumber = Math.Max(1, PageNumber);
            var query = _context.Customers
                .Select(c => new CustomerDto
                {
                    Address = c.Address,
                    BirthDay = c.BirthDay,
                    Email = c.Email,
                    Id = c.Id,
                    IsActive = c.IsActive,
                    Name = c.Name,
                    Phone = c.Phone
                });

            TotalItemsCount = await query.CountAsync();
            Customers = await query
                .OrderBy(c => c.Name)
                .Skip((PageNumber -1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}
