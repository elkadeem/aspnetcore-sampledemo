using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationSample.Web.Models;
using ApplicationSample.Web.DTO;
using ApplicationSample.Web.BusinessServices;

namespace ApplicationSample.Web.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly CustomersService _customersService;

        public DetailsModel(CustomersService customersService)
        {
            _customersService = customersService;
        }

        public CustomerDto Customer { get; set; } = default!;

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
            else
            {
                Customer = customer;
            }
            return Page();
        }
    }
}
