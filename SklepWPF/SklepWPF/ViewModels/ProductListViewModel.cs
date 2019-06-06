using SklepWPF.Data;
using SklepWPF.Models;
using SklepWPF.Enums;
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
    class ProductListViewModel : ObservableObject, IPageViewModel
    {
        private int productId;

        //dane produktu
        private string name;
        private string description;
        private double price;
        private string brand;
        private int quantity;
        private string category;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                    name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (description != value)
                    description = value;
                OnPropertyChanged("Description");
            }
        }

        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                if (price != value)
                    price = value;
                OnPropertyChanged("Price");
            }
        }

        public string Brand
        {
            get
            {
                return brand;
            }
            set
            {
                if (brand != value)
                    brand = value;
                OnPropertyChanged("Brand");
            }
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                if (quantity != value)
                    quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        public string Category
        {
            get
            {
                return category;
            }
            set
            {
                if (category != value)
                    category = value;
                OnPropertyChanged("Category");
            }
        }

        private readonly MyDbContext _db;
        private readonly int _pageSize;
        private int _productsQuantity;
        private int _page;

        public string Query { get; set; }
        public ProductListSortingProducts SortedProducts { get; set; }
        public ICollection<Category> ChosenCategories { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Product> Products { get; set; }
        public List<Product> _products { get; set; }

        public ProductListViewModel()
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

        public ICommand ChangeDisplayedItemCommand
        {
            get
            {
                return new RelayCommand(p => ChangeDisplayedItem((int)p));
            }
        }

        public void ChangeDisplayedItem(int index)
        {
            if(index >= 0)
            {
                var product = Products.ElementAt(index);
                if (product != null)
                {
                    Name = product.Name;
                    Description = product.Description;
                    Price = product.Price;
                    Brand = product.Brand;
                    Quantity = product.Quantity;
                    var pom = product.Categories.SingleOrDefault();
                    if(pom != null)
                    Category = pom.Name;
                }
            }
        }

        public ICommand UpdateSelectedProductCommand
        {
            get
            {
                return new RelayCommand(p => UpdateSelectedProduct((int)p), p => IsProductValid());
            }
        }

        public void UpdateSelectedProduct(int index)
        {
            var product = Products.ElementAt(index);
            if (product != null)
            {
                //wyciągnięcie ID produktu obecnego na liście
                productId = product.Id;

                //zaktualizowanie jego danych i wpisane do db
                var product2 = _db.Products.Find(productId);
                product2.Name = Name;
                product2.Description = Description;
                product2.Brand = Brand;
                product2.Price = Math.Round(Price,2);
                product2.Quantity = Quantity;

                var cat = _db.Categories.Where(p => p.Name == Category).ToList();
                product2.Categories = cat;

                _db.SaveChanges();
                LoadData();
            }
        }

        public ICommand DeleteSelectedProductCommand
        {
            get
            {
                return new RelayCommand(p => DeleteSelectedProduct((int)p));
            }
        }

        public void DeleteSelectedProduct(int index)
        {
            var product = Products.ElementAt(index);
            if (product != null)
            {
                //wyciągnięcie ID produktu obecnego na liście
                productId = product.Id;
                var product2 = _db.Products.Find(productId);
                _db.Products.Remove(product2);

                _db.SaveChanges();
                LoadData();
            }
        }

        public ICommand AddNewProductCommand
        {
            get
            {
                return new RelayCommand(p => AddNewProduct(Name, Description, Price, Brand, Quantity, Category));
            }
        }

        public void AddNewProduct(string name, string description, double price, string brand, int quantity, string category)
        {
            var cat = _db.Categories.Where(p => p.Name == category).SingleOrDefault();
            var newProduct = new Product(name, description, Math.Round(price, 2), brand, quantity, cat);

            _db.Products.Add(newProduct);
            _db.SaveChanges();

            LoadData();
        }

        private bool IsProductValid()
        {
            if (String.IsNullOrEmpty(Name) ||
                String.IsNullOrEmpty(Description) ||
                String.IsNullOrEmpty(Brand))
            {
                return false;
            }

            return true;
        }

        public ICommand SortProductListCommand
        {
            get
            {
                return new RelayCommand(p => SortProductList((int)p));
            }
        }

        public void SortProductList(int index)
        {
            List<Product> products;
            switch(index)
            {
                case (int)ProductListSortingProducts.NameAsc:
                    products = Products.OrderBy(p => p.Name).ToList();
                    break;
                case (int)ProductListSortingProducts.NameDesc:
                    products = Products.OrderByDescending(p => p.Name).ToList();
                    break;
                case (int)ProductListSortingProducts.PrizeAsc:
                    products = Products.OrderBy(p => p.Price).ToList();
                    break;
                case (int)ProductListSortingProducts.PrizeDesc:
                    products = Products.OrderByDescending(p => p.Price).ToList();
                    break;
                case (int)ProductListSortingProducts.QuantityAsc:
                    products = Products.OrderBy(p => p.Quantity).ToList();
                    break;
                case (int)ProductListSortingProducts.QunatityDesc:
                    products = Products.OrderByDescending(p => p.Quantity).ToList();
                    break;
                default:
                    products = _products.ToList();
                    break;
            }

            Products.Clear();
            foreach(Product p in products)
            {
                Products.Add(p);
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

        public ICommand NextPageCommand
        {
            get
            {
                return new RelayCommand(p => NextPage(),
                    p => IsValidNext());
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
            return (Page - 1) > 0;
        }

        private void PreviousPage()
        {
            Page--;
            RefreshProducts(_products);

        }

        public ICommand QueryByCategoriesCommand
        {
            get
            {
                return new RelayCommand(p => QueryByCategories((string)p));
            }
        }

        private void QueryByCategories(string categoryName)
        {
            Page = 1;
            var alreadyInCategories = ChosenCategories
                .Where(x => x.Name == categoryName)
                .SingleOrDefault();

            if (alreadyInCategories == null)
            {

                var category = _db.Categories
                    .Where(x => x.Name == categoryName)
                    .SingleOrDefault();

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

        private void LoadData()
        {
            _products = _db.Products.Include(c => c.Categories).ToList();
            RefreshProducts(_products);


            var categories = _db.Categories.ToList();
            Categories.Clear();
            foreach (var item in categories) Categories.Add(item);
        }

        private void RefreshProducts(ICollection<Product> products)
        {
            Products.Clear();

            _productsQuantity = products.Count;
            var takeProducts = products.Skip(_pageSize * (Page - 1)).Take(_pageSize);

            foreach (var item in takeProducts) Products.Add(item);
        }

        public ICommand BackCommand
        {
            get
            {
                return new RelayCommand(p => Back());
            }
        }

        private void Back()
        {
            ApplicationViewModel.Instance.CurrentPageViewModel = ApplicationViewModel.Instance.PageViewModels.SingleOrDefault(n => n.Name == "AdminPanel");
        }
    }
}
