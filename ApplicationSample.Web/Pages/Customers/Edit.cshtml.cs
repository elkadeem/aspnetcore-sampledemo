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
        public UpdateCustomerViewModel CustomerViewModel { get; set; } = default!;

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
            if (!string.IsNullOrWhiteSpace(CustomerViewModel.Email)
                 && _context.Customers.Any(c => c.Id != CustomerViewModel.Id &&  c.Email == CustomerViewModel.Email))
            {
                ModelState.AddModelError("Customer.Email", "Email already exists");
            }

            if (!string.IsNullOrWhiteSpace(CustomerViewModel.Phone)
                && _context.Customers.Any(c => c.Id != CustomerViewModel.Id && c.Phone == CustomerViewModel.Phone))
            {
                ModelState.AddModelError("Customer.Phone", "Phone already exists");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var currentCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == CustomerViewModel.Id);
            if (currentCustomer == null)
            {
                return NotFound();
            }

            currentCustomer.BirthDay = CustomerViewModel.BirthDay;
            currentCustomer.Email = CustomerViewModel.Email;
            currentCustomer.IsActive = CustomerViewModel.IsActive;
            currentCustomer.Name = CustomerViewModel.Name;
            currentCustomer.Phone = CustomerViewModel.Phone;
            currentCustomer.Address = CustomerViewModel.Address;

            if (CustomerViewModel.IdPhoto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await CustomerViewModel.IdPhoto.CopyToAsync(memoryStream);
                    currentCustomer.IdPhoto = memoryStream.ToArray();
                    currentCustomer.IdPhotoContentType = CustomerViewModel.IdPhoto.ContentType;
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(CustomerViewModel.Id))
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
