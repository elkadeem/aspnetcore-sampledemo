using ApplicationSample.Web.DTO;
using ApplicationSample.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationSample.Web.BusinessServices
{
    public class CustomersService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(ApplicationDbContext context
            , ILogger<CustomersService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<string>> Add(Customer customer)
        {
            _logger.LogDebug("Adding customer");
            if (customer is null)
            {
                _logger.LogError("Customer is null");
                throw new ArgumentNullException(nameof(customer));
            }

            List<string> errors = ValidateCustomer(customer);
            if (errors.Count > 0)
            {
                _logger.LogError("Customer validation failed");
                return errors;
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Customer added successfully");
            return new List<string>();
        }

        public async Task<List<string>> Update(int id, Customer customer)
        {
            _logger.LogWarning("Updating customer with id '{CustomerId}'", customer.Id);
            if (customer is null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            List<string> errors = ValidateCustomer(customer);
            if (errors.Count > 0)
            {
                _logger.LogError("Customer validation failed");
                return errors;
            }

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            _logger.LogWarning("Customer with id '{Id}' updated successfully"
                , customer.Id);
            return new List<string>();
        }

        private List<string> ValidateCustomer(Customer customer)
        {
            List<string> errors = new List<string>();
            if (!string.IsNullOrWhiteSpace(customer.Email)
                 && _context.Customers.Any(c => c.Id != customer.Id && c.Email == customer.Email))
            {
                errors.Add("Email already exists");
            }

            if (!string.IsNullOrWhiteSpace(customer.Phone)
                && _context.Customers.Any(c => c.Id != customer.Id && c.Phone == customer.Phone))
            {
                errors.Add("Phone already exists");
            }

            return errors;
        }

        public async Task<List<CustomerDto>> GetCustomersAsync()
        {
            return await _context.Customers
                .Select(c => new CustomerDto
                {
                    Address = c.Address,
                    BirthDay = c.BirthDay,
                    Email = c.Email,
                    Id = c.Id,
                    IsActive = c.IsActive,
                    Name = c.Name,
                    Phone = c.Phone
                }).ToListAsync();
        }

        public async Task<CustomerDto?> GetCustomerAsync(int id)
        {
            return await _context.Customers
                .Select(c => new CustomerDto
                {
                    Address = c.Address,
                    BirthDay = c.BirthDay,
                    Email = c.Email,
                    Id = c.Id,
                    IsActive = c.IsActive,
                    Name = c.Name,
                    Phone = c.Phone
                }).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CustomerExists(int id)
        {
            return await _context.Customers.AnyAsync(e => e.Id == id);
        }

        public async Task<(byte[] Photo, string ContentType)> GetCustomerPhotoAsync(int id)
        {
            var result = await _context.Customers
                .Select(c => new { c.Id, c.IdPhotoContentType, c.IdPhoto })
                .FirstOrDefaultAsync(c => c.Id == id);

            if (result != null)
            {
                return (result.IdPhoto, result.IdPhotoContentType);
            }

            return (null, string.Empty);
        }

    }
}
