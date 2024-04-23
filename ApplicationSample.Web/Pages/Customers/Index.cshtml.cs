using ApplicationSample.Web.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace ApplicationSample.Web.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationSample.Web.Models.ApplicationDbContext _context;

        public IndexModel(ApplicationSample.Web.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        //public List<CustomerDto> Customers { get; set; } = default!;

        public StaticPagedList<CustomerDto> Customers { get; set; } = default!;
        public int PageSize { get; set; } = 3;
        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalItemsCount { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Address { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }

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

            if (!string.IsNullOrWhiteSpace(Name))
            {
                query = query.Where(c => c.Name.Contains(Name));
            }

            if (!string.IsNullOrWhiteSpace(Address))
            {
                query = query.Where(c => c.Address.Contains(Address));
            }

            if(!string.IsNullOrWhiteSpace(Email))
            {
                query = query.Where(c => c.Email.Contains(Email));
            }

            //Customers = await query
            //    .OrderBy(c => c.Name)
            //    .Skip((PageNumber - 1) * PageSize)
            //    .Take(PageSize)
            //    .ToListAsync();

            TotalItemsCount = await query.CountAsync();
            var customers = await query
                .OrderBy(c => c.Name)
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();


            Customers = new StaticPagedList<CustomerDto>(customers, PageNumber, PageSize, TotalItemsCount);
        }
    }
}
