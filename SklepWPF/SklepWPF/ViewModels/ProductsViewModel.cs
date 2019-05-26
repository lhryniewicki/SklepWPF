using SklepWPF.Data;
using SklepWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Data.Entity;
using System.Windows.Input;

namespace SklepWPF.ViewModels
{
	class ProductsViewModel :ObservableObject, IPageViewModel
	{
		public string Name { get; set; }

		private readonly MyDbContext _db;

		public string Query { get; set; }

		

		private readonly int PageSize;

		private int _productsQuantity;
		private int _page;

		public ICollection<Category> ChosenCategories { get; set; } 
		public ICollection<Category> Categories { get; set; }
		public ICollection<Product> Products { get; set; }
		public List<Product> _products { get; set; }

		public ProductsViewModel()
		{

			_db = MyDbContext.Create();
			ChosenCategories = new List<Category>();
			_products = new List<Product>();
			Products = new ObservableCollection<Product>();
			Categories = new ObservableCollection<Category>();
			PageSize = 10;
			_page = 1;

		}


		public ICommand NextPageCommand
		{
			get
			{
				return new RelayCommand(p => NextPage(),
					p=> IsValidNext());
			}
		}

		private bool IsValidNext()
		{
			return (_productsQuantity > (PageSize * (_page))); 
		}

		private void NextPage()
		{
			Page++;
			RefreshProducts(_products);
		}

		//public ICommand PreviousPageCommand
		//{
		//	get
		//	{
		//		return new RelayCommand(p => NextPage(),
		//		p => IsValidPrevious());
		//	}
		//}
		public ICommand PreviousPageCommand
		{
			get
			{
				return new RelayCommand(p => PreviousPage(),
					p => IsValidPrevious());
			}
		}
		private bool IsValidPrevious()
		{
			return (Page - 1) > 0; 
		}
	
		private void PreviousPage()
		{
			Page--;
			RefreshProducts(_products);

		}

		public ICommand SearchCommand
		{
			get
			{
				return new RelayCommand(p => Search(Query));
			}
		}

		public ICommand QueryByCategoriesCommand
		{
			get
			{
				return new RelayCommand(p => QueryByCategories((string)p));
			}
		}

		public int Page
		{
			get
			{
				return _page;
			}
			set
			{
				if (_page != value)
					_page = value;
				OnPropertyChanged("Page");
			}
		}

		private void QueryByCategories(string query)
		{
			Page = 1;
			var alreadyInCategories = ChosenCategories
				.Where(x => x.Name == query)
				.SingleOrDefault();

			if(alreadyInCategories== null)
			{

				var category = _db.Categories
					.Where(x => x.Name == query)
					.SingleOrDefault();

				ChosenCategories.Add(category);
			}
			else
			{
				ChosenCategories.Remove(alreadyInCategories);
			}
			
			
			if(ChosenCategories.Count ==0)
			{
				_products = _db.Products
				.Include(x => x.Categories)
			   .ToList();
			}
			else
			{
				 _products = _db.Products
					.Include(x => x.Categories)
					.AsEnumerable()
					.Where(x => x.Categories.Any(y => ChosenCategories.Contains(y)))
					.ToList();

			}

			if (!string.IsNullOrWhiteSpace(Query))
				_products =_products.Where(x => x.Name.ToLower().
					Contains(Query.ToLower()) ||
					 x.Description.ToLower().Contains(Query.ToLower()))
					.ToList();


			RefreshProducts(_products);
		}

		private void Search(string query)
		{
			ChosenCategories.Clear();
			Page = 1;
			Categories.Clear();
			var categories = _db.Categories.ToList();
			foreach (var item in categories) Categories.Add(item);

			if (string.IsNullOrWhiteSpace(query))
			{

					 _products = _db.Products
					.Include(x => x.Categories)
					.ToList();

				RefreshProducts(_products);
			}
			else
			{
				var products = _db.Products.
					Include(x => x.Categories).
					Where(x => x.Name.ToLower().
					Contains(query.ToLower()) ||
					 x.Description.ToLower().Contains(query.ToLower()))
					.ToList();

				RefreshProducts(_products);
			}
			

		}

		private void RefreshProducts(ICollection<Product> products)
		{
			Products.Clear();
			
			_productsQuantity = products.Count;
			 var takeProducts =products.Skip(PageSize * (Page-1)).Take(PageSize);
				
			foreach (var item in takeProducts) Products.Add(item);
		}





	}
}
