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

            User user1 = new User
            {
                Id = 1,
                Name = "Kamil",
                Surname = "Kapliński",
                Nickname = "Kamil",
                PhoneNumber = "781787771",
                City = "Białystok",
                Email = "123",
                Password = "123",
                PostalCode = "111",
                PaymentMethod = Enums.PaymentMethod.ByCash,
                StreetName = "123",
                IsAdmin = true
            };

            User user2 = new User
            {
                Id = 2,
                Name = "Mati",
                Surname = "Kapliński",
                Nickname = "Mati",
                PhoneNumber = "781787771",
                City = "Białystok",
                Email = "123",
                Password = "123",
                PostalCode = "111",
                PaymentMethod = Enums.PaymentMethod.ByCash,
                StreetName = "123",
                IsAdmin = true,
            };

            User user3 = new User
            {
                Id = 3,
                Name = "Łukasz",
                Surname = "Kapliński",
                Nickname = "Łukasz",
                PhoneNumber = "781787771",
                City = "Białystok",
                Email = "123",
                Password = "123",
                PostalCode = "111",
                PaymentMethod = Enums.PaymentMethod.ByCash,
                StreetName = "123"
            };

            _db.Users.AddOrUpdate(user1);
            _db.Users.AddOrUpdate(user2);
            _db.Users.AddOrUpdate(user3);

            _db.Messages.AddOrUpdate(new Message
            {
                Id = 1,
                AuthorId = user3.Id,
                Senders = new List<User> { user3 },
                Receivers = new List<User> { user1, user2 },
                Title = "sranie do mordy",
                Content = "lalalal \n\nŁukasz Hryniewicki " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "\n\n",
                Created = DateTime.Now,
                LastModified = DateTime.Now,
                AuthorFullName = "Łukasz Hryniewicki",
                ClientSeen = true
            });

            _db.Messages.AddOrUpdate(new Message
            {
                Id = 2,
                AuthorId = user3.Id,
                Senders = new List<User> { user3 },
                Receivers = new List<User> { user1, user2 },
                Title = "sranie do mordy2",
                Content = "lalalaxxxxxxl \n\nŁukasz Hryniewicki " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "\n\n",
                Created = DateTime.Now,
                LastModified = DateTime.Now,
                AuthorFullName = "Łukasz Hryniewicki",
                ClientSeen = true
            });

            _db.Messages.AddOrUpdate(new Message
            {
                Id = 3,
                AuthorId = user3.Id,
                Senders = new List<User> { user3 },
                Receivers = new List<User> { user1, user2 },
                Title = "sranie do mordy3",
                Content = "lalasadfsdasdasdlal \n\nŁukasz Hryniewicki " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "\n\n",
                Created = DateTime.Now,
                LastModified = DateTime.Now,
                AuthorFullName = "Łukasz Hryniewicki",
                ClientSeen = true
            });

            _db.Messages.AddOrUpdate(new Message
            {
                Id = 4,
                AuthorId = user3.Id,
                Senders = new List<User> { user3 },
                Receivers = new List<User> { user1, user2 },
                Title = "sranie do mordy4",
                Content = "lalalasdsadasddwwal \n\nŁukasz Hryniewicki " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "\n\n",
                Created = DateTime.Now,
                LastModified = DateTime.Now,
                AuthorFullName = "Łukasz Hryniewicki",
                ClientSeen = true
            });

            _db.Messages.AddOrUpdate(new Message
            {
                Id = 5,
                AuthorId = user3.Id,
                Senders = new List<User> { user3 },
                Receivers = new List<User> { user1, user2 },
                Title = "sranie do mordy5",
                Content = "lalalawdwdqdqwdqwal \n\nŁukasz Hryniewicki " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "\n\n",
                Created = DateTime.Now,
                LastModified = DateTime.Now,
                AuthorFullName = "Łukasz Hryniewicki",
                ClientSeen = true
            });

            _db.Messages.AddOrUpdate(new Message
            {
                Id = 6,
                AuthorId = user3.Id,
                Senders = new List<User> { user3 },
                Receivers = new List<User> { user1, user2 },
                Title = "sranie do mordy6",
                Content = "lalalaasdasdasasdasdasdasdl \n\nŁukasz Hryniewicki " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "\n\n",
                Created = DateTime.Now,
                LastModified = DateTime.Now,
                AuthorFullName = "Łukasz Hryniewicki",
                ClientSeen = true
            });

            _db.Products.AddOrUpdate(new Product {
				Id=1,
				Brand = "Audi",
				Description = "Audi nówka sztuka nie klepana",
				Name = "Audi nówka sztuka nie klepana",
				Price = 5000,
				Quantity = 1,
				Categories = new List<Category> { carCategory } });

			_db.Products.AddOrUpdate(new Product
			{
				Id = 2,
				Brand = "Volvo",
				Description = "Volvo nówka sztuka nie klepana",
				Name = "Volvo nówka sztuka nie klepana",
				Price = 2000,
				Quantity = 1,
				Categories = new List<Category> { carCategory }
			});

			_db.Products.AddOrUpdate(new Product
			{
				Id = 3,
				Brand = "Fiat",
				Description = "Fiat nówka sztuka nie klepana",
				Name = "Fiat nówka sztuka nie klepana",
				Price = 3000,
				Quantity = 1,
				Categories = new List<Category> { carCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 4,
				Brand = "JK Rowling",
				Description = "bardzo fajna ksiazka",
				Name = "Heri pota",
				Price = 15,
				Quantity = 1,
				Categories = new List<Category> { bookCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 5,
				Brand = "JK Rowling",
				Description = "jeszcze fajniejsza ksiazka",
				Name = "Hari peta",
				Price = 30,
				Quantity = 1,
				Categories = new List<Category> { bookCategory }
			});
			_db.Products.AddOrUpdate(new Product
			{
				Id = 6,
				Brand = "JK Rowling",
				Description = "jeszcze fajniejsza ksiazka",
				Name = "Hari peta",
				Price = 30,
				Quantity = 1,
				Categories = new List<Category> { bookCategory }
			});

			_db.SaveChanges();
		}
	}
}
