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
    class ProductListViewModel : ObservableObject, IPageViewModel
    {
        private int productId;

        //dane produktu
        public string name { get; set; }
        public string description { get; set; }
        public string price { get; set; }
        public string brand { get; set; }
        public int quantity { get; set; }

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

        public string Price
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

        private readonly MyDbContext _db;
        private readonly int _pageSize;
        private int _productsQuantity;
        private int _page;

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
            var product = Products.Where(p => p.Id == (index + 1)).FirstOrDefault();
            if (product != null)
            {
                productId = product.Id;
                Name = product.Name;
                Description = product.Description;
                Price = product.Price;
                Brand = product.Brand;
                Quantity = product.Quantity;
            }
        }

        public ICommand UpdateSelectedProductCommand
        {
            get
            {
                return new RelayCommand(p => UpdateSelectedProduct((int)p));
            }
        }

        public void UpdateSelectedProduct(int index)
        {
            var product = Products.Where(p => p.Id == (index + 1)).FirstOrDefault();
            if(product != null)
            {
                //wyciągnięcie ID produktu obecnego na liście
                productId = product.Id;

                //zaktualizowanie jego danych i wpisane do db
                var product2 = _db.Products.Find(productId);
                product2.Name = Name;
                product2.Description = Description;
                product2.Brand = Brand;
                product2.Price = Price;
                product2.Quantity = Quantity;

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
            var product = Products.Where(p => p.Id == (index + 1)).FirstOrDefault();
            if (product != null)
            {
                //wyciągnięcie ID produktu obecnego na liście
                productId = product.Id;
                var product2 = _db.Products.Find(productId);
                _db.Products.Remove(product2);

                LoadData();
            }
        }

        public ICommand AddNewProductCommand
        {
            get
            {
                return new RelayCommand(p => AddNewProduct(Name, Description, Price, Brand, Quantity));
            }
        }

        public void AddNewProduct(string name, string description, string price, string brand, int quantity)
        {
            var newProduct = new Product(name, description, price, brand, quantity);

            _db.Products.Add(newProduct);
            _db.SaveChanges();

            LoadData();
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

        private void LoadData()
        {
            _products = _db.Products.ToList();
            RefreshProducts(_products);


            var categories = _db.Categories.ToList();
            foreach (var item in categories) Categories.Add(item);

        }

        private void RefreshProducts(ICollection<Product> products)
        {
            Products.Clear();

            _productsQuantity = products.Count;
            var takeProducts = products.Skip(_pageSize * (Page - 1)).Take(_pageSize);

            foreach (var item in takeProducts) Products.Add(item);
        }
    }
}
