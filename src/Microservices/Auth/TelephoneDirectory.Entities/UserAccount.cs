
using TelephoneDirectory.Global.Entities;

namespace TelephoneDirectory.Entities
{
    public class UserAccount: BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
