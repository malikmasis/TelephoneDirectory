using System.Linq;
using TelephoneDirectory.Auth.Data;
using TelephoneDirectory.Auth.Interfaces;
using TelephoneDirectory.Auth.Models;

namespace TelephoneDirectory.Auth.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAuthDbContext _reportDbContext;
        public LoginService(IAuthDbContext reportDbContext)
        {
            _reportDbContext = reportDbContext;
        }

        public bool IsAuth(UserModel userModel)
        {
            return _reportDbContext.UserAccounts.Any(p => p.UserName == userModel.Username && p.Password == userModel.Password);
        }
    }
}
