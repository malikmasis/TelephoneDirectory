using System.Collections.Generic;

namespace TelephoneDirectory.Guide.Entities
{
	public sealed class Person : BaseEntity
	{
		public string Name { get; private set; }

		public string Surname { get; private set; }

		public string Company { get; private set; }

		public double Latitude { get; private set; }

		public double Longitude { get; private set; }

		public ICollection<Contact> Contacts { get; set; } = new List<Contact>();

		public Person SetName(string company)
		{
			Name = company;

			return this;
		}

		public Person SetSurname(string surname)
		{
			Surname = surname;

			return this;
		}

		public Person SetCompany(string company)
		{
			Company = company;

			return this;
		}

		public Person SetLatitude(double latitude)
		{
			Latitude = latitude;

			return this;
		}

		public Person SetLongitude(double longitude)
		{
			Longitude = longitude;

			return this;
		}
	}
}