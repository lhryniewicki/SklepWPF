using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SklepWPF.Models
{
	class Adress
	{
		public int Id { get; set; }
		public string StreetName { get; set; }
		public string City { get; set; }
		public string PostalCode { get; set; }

		public ICollection<User> Users { get; set; }
	}
}
