using ApplicationSample.Web.Models;
using ApplicationSample.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ApplicationSample.Web.Pages.Customers
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
        public AddCustomerViewModel CustomerViewModel { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrWhiteSpace(CustomerViewModel.Email)
                 && _context.Customers.Any(c => c.Email == CustomerViewModel.Email))
            {
                ModelState.AddModelError("CustomerViewModel.Email", "Email already exists");
            }

            if (!string.IsNullOrWhiteSpace(CustomerViewModel.Phone)
                && _context.Customers.Any(c => c.Phone == CustomerViewModel.Phone))
            {
                ModelState.AddModelError("CustomerViewModel.Phone", "Phone already exists");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Customer newCustomer = new Customer
            {
                Name = CustomerViewModel.Name,
                Address = CustomerViewModel.Address,
                Email = CustomerViewModel.Email,
                Phone = CustomerViewModel.Phone,
                BirthDay = CustomerViewModel.BirthDay,
                IsActive = CustomerViewModel.IsActive
            };

            if (CustomerViewModel.IdPhoto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await CustomerViewModel.IdPhoto.CopyToAsync(memoryStream);
                    newCustomer.IdPhoto = memoryStream.ToArray();
                    newCustomer.IdPhotoContentType = CustomerViewModel.IdPhoto.ContentType;
                }
            }

            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
