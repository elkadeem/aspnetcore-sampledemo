using System.ComponentModel.DataAnnotations;

namespace ApplicationSample.Web.Models
{
    public class Deparment
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
    }
}
