using ApplicationSample.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationSample.Web.Controllers
{
    public class CustomersValidationController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CustomersValidationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyEmail([ModelBinder(Name = "CustomerViewModel.Email")]string email, [ModelBinder(Name = "CustomerViewModel.Id")]int? id)
        {
            int customerId = id ?? 0;
            if (_context.Customers.Any(c => c.Id != customerId && c.Email == email))
            {
                return Json($"Email {email} is already in use.");
            }

            return Json(true);
        }
    }
}
