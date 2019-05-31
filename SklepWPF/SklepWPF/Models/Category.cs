using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SklepWPF.Models
{
	public class Category
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }

		public ICollection<Product> Products { get; set; }
	}
}
