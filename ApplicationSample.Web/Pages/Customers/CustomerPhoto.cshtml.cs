using ApplicationSample.Web.BusinessServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ApplicationSample.Web.Pages.Customers
{
    public class CustomerPhotoModel : PageModel
    {
        private readonly CustomersService _customersService;
        public CustomerPhotoModel(CustomersService customersService)
        {
            _customersService = customersService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null && id <= 0)
            {
                return NotFound();
            }

            var result = await _customersService.GetCustomerPhotoAsync(id.Value);

            if (result.Photo == null
                || string.IsNullOrWhiteSpace(result.ContentType))
            {
                return NotFound();
            }

            return File(result.Photo, result.ContentType);
        }
    }
}
