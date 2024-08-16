namespace Wpf_Wcl.Models
{
    using System.Security;

    public class User
    {
        public int Id { get; set; }
        public SecureString Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
