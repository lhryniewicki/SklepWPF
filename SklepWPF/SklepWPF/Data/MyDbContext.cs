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
		public DbSet<UserCart> UsersCart { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
			modelBuilder.Entity<UserCart>()
				.HasKey(x => new { x.ProductId, x.UserId });

			modelBuilder.Entity<UserCart>()
				.HasRequired(x => x.Product)
				.WithMany(x => x.InUserCart)
				.HasForeignKey(x => x.ProductId);

			modelBuilder.Entity<UserCart>()
				.HasRequired(x => x.User)
				.WithMany(x => x.Cart)
				.HasForeignKey(x => x.UserId);



			modelBuilder.Entity<Message>()
                .HasMany(s => s.Senders)
                .WithMany(u => u.SentMessages);

            modelBuilder.Entity<Message>()
                .HasMany(r => r.Receivers)
                .WithMany(rm => rm.ReceivedMessages);

            modelBuilder.Entity<User>()
                .HasMany(o => o.Orders)
                .WithRequired(u => u.User)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .HasMany(p => p.ObservedProducts)
                .WithMany(u => u.ObservingUsers).Map(cs =>
                {
                    cs.MapLeftKey("UserId");
                    cs.MapRightKey("ProductId");
                    cs.ToTable("UserObservedProducts");
                });

            modelBuilder.Entity<Product>()
                .HasMany(o => o.Orders)
                .WithRequired(p => p.Product)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<Order>()
                .HasMany(p => p.OrderedProducts)
                .WithRequired(o => o.Order)
                .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasKey(k => new { k.OrderId, k.ProductId });  
        }
    }
}
