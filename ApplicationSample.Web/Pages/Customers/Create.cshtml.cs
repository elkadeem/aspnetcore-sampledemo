using ApplicationSample.Web.BusinessServices;
using ApplicationSample.Web.Models;
using ApplicationSample.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ApplicationSample.Web.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly CustomersService _customersService;

        public CreateModel(CustomersService customersService)
        {
            _customersService = customersService;
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

            var result = await _customersService.Add(newCustomer);
            if (result.Count == 0)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                foreach (var error in result)
                {
                    ModelState.AddModelError(string.Empty, error);
                }

                return Page();
            }
        }
    }
}
