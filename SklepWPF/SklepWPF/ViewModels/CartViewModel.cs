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
    class CartViewModel : ObservableObject, IPageViewModel
    {
        //dane produktu
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Brand { get; set; }

        private readonly MyDbContext _db;

        public ICollection<Product> ProductsInCart { get; set; }
        public List<Product> _productsInCart { get; set; }

        public CartViewModel()
        {
            _db = MyDbContext.Create();

            ProductsInCart = new ObservableCollection<Product>();
            _productsInCart = new List<Product>();

            LoadData();
        }

        private void LoadData()
        {
            var user = _db.Users.Include(c => c.Cart).SingleOrDefault(n => n.Name == RunTimeInfo.Instance.Username);

            foreach (UserCart p in user.Cart)
            {
                _productsInCart.Add(_db.Products.Where(u => u.Id == p.ProductId).FirstOrDefault());
            }
			
            RefreshProducts(_productsInCart);
        }

        private void RefreshProducts(ICollection<Product> products)
        {
            ProductsInCart.Clear();

            foreach (var item in products)
                ProductsInCart.Add(item);
        }

        public ICommand DeleteFromCartCommand
        {
            get
            {
                return new RelayCommand(p => DeleteFromCart((int)p));
            }
        }

        public void DeleteFromCart(int index)
        {
            var product = ProductsInCart.Where(u => u.Id == index).FirstOrDefault();
            var user = _db.Users.Include(c => c.Cart).SingleOrDefault(n => n.Name == RunTimeInfo.Instance.Username);
            var userCartProduct = user.Cart.Where(p => p.ProductId == product.Id).FirstOrDefault();

            user.Cart.Remove(userCartProduct);
            ProductsInCart.Remove(product);

            _db.SaveChanges();
        }

        public ICommand FinalizeOrderCommand
        {
            get
            {
                return new RelayCommand(p => FinalizeOrder(), p => ProductsInCart.Count > 0);
            }
        }

        public void FinalizeOrder()
        {
            ApplicationViewModel.Instance.CurrentPageViewModel = new OrderViewModel();
        }
    }
}
