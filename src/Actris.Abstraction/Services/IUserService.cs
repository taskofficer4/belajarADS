using System.Threading.Tasks;
using Actris.Abstraction.Model.Response;

namespace Actris.Abstraction.Services
{
    public interface IUserService
    {
        Task<User> GetCurrentUserInfo();
        Task<User> GetUserInfo(string username, bool useCache = false);
    }
}
