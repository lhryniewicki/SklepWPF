using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SklepWPF.Models
{
	class User
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Surname { get; set; }
		[Required]
		public Adress Adress { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public string Email { get; set; }

	}
}
