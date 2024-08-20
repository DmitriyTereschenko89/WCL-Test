using System.Security;

namespace Wpf_Wcl.Data_Transfer_Objects
{
    public class UserDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public SecureString Password { get; set; }
        public string Phone { get; set; }
        public int UserStatus { get; set; } = 1;
    }
}
