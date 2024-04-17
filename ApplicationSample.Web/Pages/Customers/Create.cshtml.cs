﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationSample.Web.Models;
using ApplicationSample.Web.ViewModels;

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