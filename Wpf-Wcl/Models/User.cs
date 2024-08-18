namespace Wpf_Wcl.Models
{
    using System.Security;

    public class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public SecureString Password { get; set; }
        public string PhoneNumber { get; set; }
        public int UserStatus { get; set; } = 1;
    }
}
