using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TelephoneDirectory.Auth.Data;
using TelephoneDirectory.Auth.Interfaces;
using TelephoneDirectory.Auth.Models;

namespace TelephoneDirectory.Auth.Services;

public sealed class LoginService : ILoginService
{
    private readonly IAuthDbContext _reportDbContext;
    public LoginService(IAuthDbContext reportDbContext)
    {
        _reportDbContext = reportDbContext;
    }

    public async Task<bool> IsAuthAsync(UserModel userModel)
    {
        return await _reportDbContext
               .UserAccounts
               .AnyAsync(p => p.UserName == userModel.Username && p.Password == userModel.Password);
    }
}
