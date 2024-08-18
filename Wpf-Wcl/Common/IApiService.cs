using System.Net;
using Wpf_Wcl.Models;

namespace Wpf_Wcl.Common
{
    public interface IApiService
    {
        Task<User> LoginAsync(NetworkCredential networkCredential);
        Task<User> RegisterAsync(User user);
        Task LogoutAsync();
        Task<User> GetUserAsync(string username);
    }
}
