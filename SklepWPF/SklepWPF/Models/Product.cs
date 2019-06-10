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
		public double Price { get; set; }
		[Required]
		public string Brand { get; set; }
		[Required]
		public int Quantity { get; set; }
		[Required]
		public string ImagePath { get; set; }

		public ICollection<Category> Categories { get; set;}
		public ICollection<UserCart> InUserCart { get; set; }
        public ICollection<User> ObservingUsers { get; set; }
        public ICollection<OrderProduct> Orders { get; set; }

        public Product()
        {

        }

        public Product(string name, string description, double price, string brand, int quantity, Category category)
        {
            Name = name;
            Description = description;
            Price = price;
            Brand = brand;
            Quantity = quantity;
            Categories = new List<Category>();
            Categories.Add(category);
        }
    }
}
