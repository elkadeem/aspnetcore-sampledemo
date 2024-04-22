using System.ComponentModel.DataAnnotations;

namespace ApplicationSample.Web.Models
{
    public class Name
    {
        public Name(string firstName, string fatherName, string middleName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException($"'{nameof(firstName)}' cannot be null or whitespace.", nameof(firstName));
            }

            if (string.IsNullOrWhiteSpace(fatherName))
            {
                throw new ArgumentException($"'{nameof(fatherName)}' cannot be null or whitespace.", nameof(fatherName));
            }

            if (string.IsNullOrWhiteSpace(middleName))
            {
                throw new ArgumentException($"'{nameof(middleName)}' cannot be null or whitespace.", nameof(middleName));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException($"'{nameof(lastName)}' cannot be null or whitespace.", nameof(lastName));
            }

            FirstName = firstName;
            LastName = lastName;
            FatherName = fatherName;
            MiddleName = middleName;
        }

        [MaxLength(50)]
        public string FirstName { get; private set; }

        [MaxLength(50)]
        public string LastName { get; private set; }

        [MaxLength(50)]
        public string FatherName { get; private set; }

        [MaxLength(50)]
        public string MiddleName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {FatherName} {MiddleName} {LastName}";
        }
    }
}
