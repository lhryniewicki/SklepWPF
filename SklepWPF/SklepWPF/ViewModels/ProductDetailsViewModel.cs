using SklepWPF.Data;
using SklepWPF.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;
namespace SklepWPF.ViewModels
{
	 class ProductDetailsViewModel:ObservableObject,IPageViewModel
	{
		private readonly MyDbContext _db;

		public string Name { get ; set; }

		public string ProductName { get; set; }

		public string Description { get; set; }

		public double Price { get; set; }

		public string Brand { get; set; }

		public int Quantity { get; set; }

		public ICollection<Category> Categories { get; set; }

		private bool _showCartButon = false;

		private Product _product;
		public ProductDetailsViewModel(int id)
		{
			Name = "Display";
			_db = MyDbContext.Create();
			LoadProduct(id);
			ProductName = _product.Name;
			Description = _product.Description;
			Price = _product.Price;
			Brand = _product.Brand;
			Quantity = _product.Quantity;
			Categories = _product.Categories;

		}
		public bool ShowCartButton
		{
			get
			{
				var username = new RunTimeInfo().UsernameCodeValue;
				if (username != "Konto") ShowCartButton = true;

				return _showCartButon;
			}
			set
			{
				if (_showCartButon != value)
				{
					_showCartButon = value;
					OnPropertyChanged("ShowCartButton");
				}

			}
		}

		private void LoadProduct(int id)
		{
			_product = _db.Products
				.Include(x => x.Categories)
				.Where(x => x.Id == id)
				.SingleOrDefault();		
		}
	}
}
