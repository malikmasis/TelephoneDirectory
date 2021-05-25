using System.Linq;
using System.Threading.Tasks;
using TelephoneDirectory.Auth.Data;
using TelephoneDirectory.Auth.Interfaces;

namespace TelephoneDirectory.Auth.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAuthDbContext _reportDbContext;
        public LoginService(IAuthDbContext reportDbContext)
        {
            _reportDbContext = reportDbContext;
        }

        public async Task Save()
        {
            _reportDbContext.UserAccounts.Any(p => p.UserName == "" && p.Password == "");
        }
    }
}
