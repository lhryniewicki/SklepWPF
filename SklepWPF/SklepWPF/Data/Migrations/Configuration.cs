namespace SklepWPF.Migrations
{
	using SklepWPF.Data;
	using SklepWPF.Models;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SklepWPF.Data.MyDbContext>
    {
		private readonly MyDbContext _db= MyDbContext.Create();
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
			this.MigrationsDirectory = @"Data\Migrations";
		}

        protected override void Seed(SklepWPF.Data.MyDbContext context)
        {
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data.

			var carCategory = new Category { Id=1 ,Name = "Samochody" };
			var bookCategory = new Category { Id = 2, Name = "Książki" };
			_db.Categories.AddOrUpdate(new Category { Id = 3, Name = "Elektronika" });
			_db.Categories.AddOrUpdate(carCategory);
			_db.Categories.AddOrUpdate(bookCategory);


			_db.Products.AddOrUpdate(new Product {
				Id=1,
				Brand = "Audi",
				Description = "Audi nówka sztuka nie klepana",
				Name = "Audi nówka sztuka nie klepana",
				Price = "5000zł",
				Quantity = 1,
				Categories = new List<Category> { carCategory } });

			_db.Products.AddOrUpdate(new Product
			{
				Id = 2,
				Brand = "Volvo",
				Description = "Volvo nówka sztuka nie klepana",
				Name = "Volvo nówka sztuka nie klepana",
				Price = "2000zł",
				Quantity = 1,
				Categories = new List<Category> { carCategory }
			});

			_db.Products.AddOrUpdate(new Product
			{
				Id = 3,
				Brand = "Fiat",
				Description = "Fiat nówka sztuka nie klepana",
				Name = "Fiat nówka sztuka nie klepana",
				Price = "3000zł",
				Quantity = 1,
				Categories = new List<Category> { carCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 4,
				Brand = "JK Rowling",
				Description = "bardzo fajna ksiazka",
				Name = "Heri pota",
				Price = "15zł",
				Quantity = 1,
				Categories = new List<Category> { bookCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 5,
				Brand = "JK Rowling",
				Description = "jeszcze fajniejsza ksiazka",
				Name = "Hari peta",
				Price = "30zł",
				Quantity = 1,
				Categories = new List<Category> { bookCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 6,
				Brand = "JK Rowling",
				Description = "jeszcze fajniejsza ksiazka",
				Name = "Hari peta",
				Price = "30zł",
				Quantity = 1,
				Categories = new List<Category> { bookCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 7,
				Brand = "JK Rowling",
				Description = "jeszcze fajniejsza ksiazka",
				Name = "Hari peta",
				Price = "30zł",
				Quantity = 1,
				Categories = new List<Category> { bookCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 8,
				Brand = "JK Rowling",
				Description = "jeszcze fajniejsza ksiazka",
				Name = "Hari peta",
				Price = "30zł",
				Quantity = 1,
				Categories = new List<Category> { bookCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 9,
				Brand = "JK Rowling",
				Description = "jeszcze fajniejsza ksiazka",
				Name = "Hari peta",
				Price = "30zł",
				Quantity = 1,
				Categories = new List<Category> { bookCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 10,
				Brand = "JK Rowling",
				Description = "jeszcze fajniejsza ksiazka",
				Name = "Hari peta",
				Price = "30zł",
				Quantity = 1,
				Categories = new List<Category> { bookCategory }
			}); _db.Products.AddOrUpdate(new Product
			{
				Id = 11,
				Brand = "JK Rowling",
				Description = "jeszcze fajniejsza ksiazka",
				Name = "Hari peta",
				Price = "30zł",
				Quantity = 1,
				Categories = new List<Category> { bookCategory }
			});

			_db.SaveChanges();
		}
	}
}
