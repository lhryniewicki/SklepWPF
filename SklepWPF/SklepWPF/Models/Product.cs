using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SklepWPF.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Price { get; set; }
		public string Brand { get; set; }
		public int Quantity { get; set; }
		public ICollection<Category> Categories { get;set;}
        public ICollection<OrderProduct> Orders { get; set; }
    }
}
