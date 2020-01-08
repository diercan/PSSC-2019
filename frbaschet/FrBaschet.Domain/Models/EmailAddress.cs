namespace FrBaschet.Domain.Models
{
    public class EmailAddress
    {
        public EmailAddress()
        {
        }

        public EmailAddress(string email, string name = null)
        {
            Email = email;
            Name = name;
        }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}