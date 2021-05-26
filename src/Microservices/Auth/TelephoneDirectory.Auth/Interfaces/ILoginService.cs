using TelephoneDirectory.Auth.Models;

namespace TelephoneDirectory.Auth.Interfaces
{
    public interface ILoginService
    {
        bool IsAuth(UserModel userModel);
    }
}
