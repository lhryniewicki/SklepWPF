using SklepWPF.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace SklepWPF.Data
{
	class MyDbContext :DbContext
	{
		public MyDbContext() : base("Default")
		{
		}
		public static MyDbContext Create()
		{
			return new MyDbContext();
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
	}
}
