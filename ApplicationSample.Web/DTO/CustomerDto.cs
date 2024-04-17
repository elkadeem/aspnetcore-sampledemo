using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApplicationSample.Web.DTO
{
    public class CustomerDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
       
        public string? Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        [DisplayName("Birth day")]
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        [DisplayName("Is active")]
        public bool IsActive { get; set; }
    }
}
