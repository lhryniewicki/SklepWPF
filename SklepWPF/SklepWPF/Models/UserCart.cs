using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SklepWPF.Models
{
	public class UserCart
	{
		public int Quantity { get; set; }

		public int UserId { get; set; }
		public User User { get; set; }

		public int ProductId { get; set; }
		public Product Product { get; set; }

		
	}
}
