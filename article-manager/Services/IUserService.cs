using System.Threading.Tasks;
using backendlib.Models;

namespace article_manager.Services
{
    public interface IUserService
    {
        Task<User> GetUser(int id);
        Task<string> Login(User user);
    }
}