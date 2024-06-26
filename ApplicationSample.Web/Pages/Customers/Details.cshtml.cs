﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationSample.Web.Models;
using ApplicationSample.Web.DTO;

namespace ApplicationSample.Web.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationSample.Web.Models.ApplicationDbContext _context;

        public DetailsModel(ApplicationSample.Web.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public CustomerDto Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null && id <= 0)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Select(c => new CustomerDto
                {
                    Address = c.Address,
                    BirthDay = c.BirthDay,
                    Email = c.Email,
                    Id = c.Id,
                    IsActive = c.IsActive,
                    Name = c.Name,
                    Phone = c.Phone
                }).FirstOrDefaultAsync(m => m.Id == id);
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
