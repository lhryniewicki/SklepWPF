using SklepWPF.Data;
using SklepWPF.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;
using System.Windows.Input;

namespace SklepWPF.ViewModels
{
	 class ProductDetailsViewModel:ObservableObject,IPageViewModel
	{
		private readonly MyDbContext _db;

		public ProductsViewModel ProductsViewModel { get; set; }

		public int Id { get; set; }

		public string Name { get ; set; }

		public string ProductName { get; set; }

		public string Description { get; set; }

		public double Price { get; set; }

		public string Brand { get; set; }

		public int Quantity { get; set; }

		public string ImagePath { get; set; }

		public ICollection<Category> Categories { get; set; }

		private bool _showButton = false;

		private Product _product;
		public ProductDetailsViewModel(int id)
		{
			Name = "Display";
			_db = MyDbContext.Create();
			LoadProduct(id);
			Id = _product.Id;
			ProductName = _product.Name;
			Description = _product.Description;
			Price = _product.Price;
			Brand = _product.Brand;
			Quantity = _product.Quantity;
			Categories = _product.Categories;
			ImagePath = _product.ImagePath;
			ProductsViewModel = new ProductsViewModel();

		}
		public ProductDetailsViewModel()
		{

		}
		public bool ShowButton
		{
			get
			{
				var username = new RunTimeInfo().UsernameCodeValue;
				if (username != "Konto") ShowButton = true;

				return _showButton;
			}
			set
			{
				if (_showButton != value)
				{
					_showButton = value;
					OnPropertyChanged("ShowButton");
				}

			}
		}

		private void LoadProduct(int id)
		{
			_product = _db.Products
				.Include(x => x.Categories)
				.SingleOrDefault(x => x.Id == id);		
		}

	}
}
