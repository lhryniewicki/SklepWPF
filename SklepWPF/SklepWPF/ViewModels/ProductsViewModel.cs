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


		private readonly int _pageSize;
		private int _productsQuantity;
		private int _page;
		private bool _showButton=false;

		public ICollection<Category> ChosenCategories { get; set; } 
		public ICollection<Category> Categories { get; set; }
		public ICollection<Product> Products { get; set; }
		private List<Product> _products { get; set; }

		public ProductsViewModel()
		{

			_db = MyDbContext.Create();

			ChosenCategories = new List<Category>();
			Products = new ObservableCollection<Product>();
			Categories = new ObservableCollection<Category>();
			_products = new List<Product>();

			_pageSize = 10;
			Page = 1;

			LoadData();


		}

		public bool ShowButton
		{
			get {
				var username = new RunTimeInfo().UsernameCodeValue;
				if (username != "Konto") ShowButton = true;

				return _showButton;
			}
			set
			{
				if(_showButton != value)
				{
					_showButton = value;
					OnPropertyChanged("ShowButton");
				}

			}
		}

		private void LoadData()
		{
			 _products = _db.Products.ToList();
			RefreshProducts(_products);
			

			var categories = _db.Categories.ToList();
			foreach (var item in categories) Categories.Add(item);

		}
		public ICommand ObserveItemCommand
		{
			get
			{
				return new RelayCommand(p => ObserveItem((int)p));
			}
		}
		private void ObserveItem(int id)
		{
			var nickname = RunTimeInfo.Instance.Username;

			var user = _db.Users.
				Include(x => x.ObservedProducts)
				.SingleOrDefault(x => x.Nickname == nickname);

			var item = _db.Products
				.SingleOrDefault(x=>x.Id == id);

			user.ObservedProducts.Add(item);
			_db.SaveChanges();
		}


		public ICommand AddItemToCartCommand
		{
			get
			{
				return new RelayCommand(p => AddItemToCart((int)p));
			}
		}
		public void AddItemToCart(int id)
		{
			

			var user = _db.Users.
				Include(x=>x.Cart)
				.SingleOrDefault(x=>x.Nickname == RunTimeInfo.Instance.Username);

			var item = _db.Products
				.SingleOrDefault(x=>x.Id == id);

			var userCartExists = _db.UsersCart
				.SingleOrDefault(x => x.UserId == user.Id && item.Id == x.ProductId);

			if(userCartExists == null)
			{
				var userCart = new UserCart { Product = item, User = user,Quantity=1 };
				user.Cart.Add(userCart);
			}
			else
			{
				userCartExists.Quantity++;
			}

			
			_db.SaveChanges();


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
			return (_productsQuantity > (_pageSize * (_page))); 
		}

		private void NextPage()
		{
			Page++;
			RefreshProducts(_products);
		}
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
			return (Page-1) > 0; 
		}
	
		private void PreviousPage()
		{
			Page--;
			RefreshProducts(_products);

		}
		public ICommand DisplayProductCommand
		{
			get
			{
				return new RelayCommand(p => DisplayProduct((int)p));
			}
		}
		public void DisplayProduct(int id)
		{
			ApplicationViewModel.Instance.CurrentPageViewModel = new ProductDetailsViewModel(id);
		}

		public ICommand SearchCommand
		{
			get
			{
				return new RelayCommand(p => Search());
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

		private void QueryByCategories(string categoryName)
		{
			Page = 1;
			var alreadyInCategories = ChosenCategories
				.SingleOrDefault(x => x.Name == categoryName);

			if(alreadyInCategories== null)
			{

				var category = _db.Categories
					.SingleOrDefault(x => x.Name == categoryName);

				ChosenCategories.Add(category);
			}
			else
			{
				ChosenCategories.Remove(alreadyInCategories);
			}


			Search();
		}

		private void Search()
		{

			Page = 1;

			_products = _db.Products
				.Include(x => x.Categories)
				.ToList();

			if (ChosenCategories.Count > 0)
			{
				_products = _products
					   .Where(x => x.Categories.Any(y => ChosenCategories.Contains(y)))
					   .ToList();
			}

			if (!string.IsNullOrWhiteSpace(Query))
				_products = _products.Where(x => x.Name.ToLower().
					 Contains(Query.ToLower()) ||
					  x.Description.ToLower().Contains(Query.ToLower()))
					.ToList();

			RefreshProducts(_products);
		}

		private void RefreshProducts(ICollection<Product> products)
		{
			Products.Clear();
			
			_productsQuantity = products.Count;
			 var takeProducts =products.Skip(_pageSize * (Page-1)).Take(_pageSize);
				
			foreach (var item in takeProducts) Products.Add(item);
		}





	}
}
