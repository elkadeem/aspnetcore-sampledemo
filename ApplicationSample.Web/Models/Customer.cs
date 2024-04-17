namespace ApplicationSample.Web.Models
{
    public class Customer
    {
       public int Id { get; set; }

        public string Name { get; set; }

        public string? Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime BirthDay { get; set; }

        public bool IsActive { get; set; }
        
        public byte[]? IdPhoto { get; set; }

        public string? IdPhotoContentType { get; set; }
    }
}
