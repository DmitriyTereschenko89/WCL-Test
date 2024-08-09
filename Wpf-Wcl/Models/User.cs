namespace Wpf_Wcl.Models
{
    // id
    //    имя
    //  фамилия
    //email
    //телефон

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
