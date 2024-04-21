using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApplicationSample.Web.Models;
using ApplicationSample.Web.ViewModels;

namespace ApplicationSample.Web.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly ApplicationSample.Web.Models.ApplicationDbContext _context;

        public EditModel(ApplicationSample.Web.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UpdateCustomerViewModel Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null && id <= 0)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Select(c => new Customer
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    Email = c.Email,
                    Phone = c.Phone,
                    BirthDay = c.BirthDay,
                    IsActive = c.IsActive,
                })
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            Customer = new UpdateCustomerViewModel
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

            var currentCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == Customer.Id);
            if (currentCustomer == null)
            {
                return NotFound();
            }

            currentCustomer.BirthDay = Customer.BirthDay;
            currentCustomer.Email = Customer.Email;
            currentCustomer.IsActive = Customer.IsActive;
            currentCustomer.Name = Customer.Name;
            currentCustomer.Phone = Customer.Phone;
            currentCustomer.Address = Customer.Address;

            if (Customer.IdPhoto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Customer.IdPhoto.CopyToAsync(memoryStream);
                    currentCustomer.IdPhoto = memoryStream.ToArray();
                    currentCustomer.IdPhotoContentType = Customer.IdPhoto.ContentType;
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(Customer.Id))
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

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
