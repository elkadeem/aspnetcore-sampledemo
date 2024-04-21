using ApplicationSample.Web.BusinessServices;
using ApplicationSample.Web.DTO;
using ApplicationSample.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ApplicationSample.Web.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        private readonly CustomersService _customersService;

        public DeleteModel(CustomersService customersService)
        {
            _customersService = customersService;
        }

        [BindProperty]
        public CustomerDto Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customersService.GetCustomerAsync(id.Value);
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

            await _customersService.DeleteCustomerAsync(id.Value);
            return RedirectToPage("./Index");
        }
    }
}
