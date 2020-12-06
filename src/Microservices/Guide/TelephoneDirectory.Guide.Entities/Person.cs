using System.Collections.Generic;
using TelephoneDirectory.Global.Entities;

namespace TelephoneDirectory.Guide.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
