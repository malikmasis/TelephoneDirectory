using System.Threading.Tasks;
using TelephoneDirectory.Auth.Models;

namespace TelephoneDirectory.Auth.Interfaces;

public interface ILoginService
{
    Task<bool> IsAuthAsync(UserModel userModel);
}
