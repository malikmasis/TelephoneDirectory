using TelephoneDirectory.Global.Entities;

namespace TelephoneDirectory.Guide.Entities
{
    public class Contact: BaseEntity
    {
        public long PersonId{ get; set; }
        public InfoType InfoType { get; set; }
        public string Info { get; set; }
    }

    public enum InfoType
    {
        Phone = 1,
        Email,
        Location
    }
}