using System.ComponentModel.DataAnnotations;

namespace ApplicationSample.Web.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public Name Name { get; set; }

        [MaxLength(50)]
        public string EmployeeNumber { get; set; }

        public int DepartmentId { get; set; }

        public bool IsActive { get; set; }

        [MaxLength(20)]
        public string NationalId { get; set; }

        public DateTime HireDate { get; set; }

        public byte[]? IdPhoto { get; set; }

        [MaxLength(50)]
        public string? IdPhotoContentType { get; set; }

        [MaxLength(100)] 
        public string Email { get; set; }

        [MaxLength(20)] 
        public string Mobile { get; set; }

        public Deparment Deparment { get; set; }

    }
}
