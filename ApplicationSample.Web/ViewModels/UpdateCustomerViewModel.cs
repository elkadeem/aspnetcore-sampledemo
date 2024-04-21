using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApplicationSample.Web.ViewModels
{
    public class UpdateCustomerViewModel
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        [Remote(action: "VerifyEmail", controller: "CustomersValidation"
            , AdditionalFields = nameof(Id))]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        [DisplayName("Birth Day")]
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        [DisplayName("ID Photo")]
        public IFormFile? IdPhoto { get; set; }
    }
}
