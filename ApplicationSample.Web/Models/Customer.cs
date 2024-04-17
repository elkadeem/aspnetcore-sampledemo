using System.ComponentModel.DataAnnotations;

namespace ApplicationSample.Web.Models
{
    public class Customer
    {
       public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime BirthDay { get; set; }

        public bool IsActive { get; set; }
        
        public byte[]? IdPhoto { get; set; }

        [MaxLength(50)]
        public string? IdPhotoContentType { get; set; }
    }
}
