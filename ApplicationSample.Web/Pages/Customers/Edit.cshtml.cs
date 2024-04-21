using ApplicationSample.Web.BusinessServices;
using ApplicationSample.Web.Models;
using ApplicationSample.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ApplicationSample.Web.Pages.Customers
{
    public class EditModel : PageModel
    {
        
        private readonly CustomersService _customersService;

        public EditModel(CustomersService customersService)
        {           
            _customersService = customersService;
        }

        [BindProperty]
        public UpdateCustomerViewModel CustomerViewModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null && id <= 0)
            {
                return NotFound();
            }

            var customer = await _customersService.GetCustomerAsync(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            CustomerViewModel = new UpdateCustomerViewModel
            {
                Address = customer.Address,
                BirthDay = customer.BirthDay,
                Email = customer.Email,
                Id = customer.Id,
                IsActive = customer.IsActive,
                Name = customer.Name,
                Phone = customer.Phone
            };
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var currentCustomer = await _customersService.GetCustomerAsync(CustomerViewModel.Id);
            if (currentCustomer == null)
            {
                return NotFound();
            }

            var customer = new Customer
            {
                Id = CustomerViewModel.Id,
                Address = CustomerViewModel.Address,
                BirthDay = CustomerViewModel.BirthDay,
                Email = CustomerViewModel.Email,
                IsActive = CustomerViewModel.IsActive,
                Name = CustomerViewModel.Name,
                Phone = CustomerViewModel.Phone
            };
            
            if (CustomerViewModel.IdPhoto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await CustomerViewModel.IdPhoto.CopyToAsync(memoryStream);
                    customer.IdPhoto = memoryStream.ToArray();
                    customer.IdPhotoContentType = CustomerViewModel.IdPhoto.ContentType;
                }
            }

            try
            {
                var result = await _customersService.Update(customer.Id
                    , customer); 
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _customersService.CustomerExists(CustomerViewModel.Id))
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

    }
}
