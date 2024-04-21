using ApplicationSample.Web.BusinessServices;
using ApplicationSample.Web.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ApplicationSample.Web.Pages.Customers
{
    public class IndexModel : PageModel
    {
        
        private readonly CustomersService _customersService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(CustomersService customersService
            , ILogger<IndexModel> logger)
        {
           
            _customersService = customersService;
            _logger = logger;
        }

        public IList<CustomerDto> Customers { get;set; } = default!;

        public async Task OnGetAsync()
        {
            try
            {
                Customers = await _customersService.GetCustomersAsync();
                _logger.LogInformation("Retrive '{count}' customers.", Customers.Count);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An error occurred while getting customers");
                throw;
            }            
        }
    }
}
