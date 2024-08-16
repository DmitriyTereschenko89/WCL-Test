namespace Wpf_Wcl.Common
{
    using Wpf_Wcl.Models;

    public interface IApiService
    {
        Task<User> LoginAsync(string userName, string password);
        Task<User> RegisterAsync(User user);
        Task LogoutAsync();
        Task<User> GetUserAsync(string username);
    }
}
