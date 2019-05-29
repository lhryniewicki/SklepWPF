using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SklepWPF.Models
{
	public class Product
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public string Price { get; set; }
		[Required]
		public string Brand { get; set; }
		[Required]
		public int Quantity { get; set; }

		public ICollection<Category> Categories { get; set;}
		public ICollection<User> InUserCart { get; set; }
	}
}
