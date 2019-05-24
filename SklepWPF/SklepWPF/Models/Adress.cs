using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SklepWPF.Models
{
	class Adress
	{
		public int Id { get; set; }
		[Required]
		public string StreetName { get; set; }
		[Required]
		public string City { get; set; }
		[Required]
		public string PostalCode { get; set; }

		public ICollection<User> Users { get; set; }
	}
}
