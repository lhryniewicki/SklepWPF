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

		DbSet<User> Users { get; set; }
	}
}
