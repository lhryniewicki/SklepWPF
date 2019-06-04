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
        public string Name { get; set; }

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
