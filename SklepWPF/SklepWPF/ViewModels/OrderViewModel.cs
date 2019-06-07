using SklepWPF.Data;
using SklepWPF.Enums;
using SklepWPF.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SklepWPF.ViewModels
{
    class OrderViewModel : ObservableObject, IPageViewModel
    {
        //dane osobowe użytkownika
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string StreetName { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }

        //dane produktów
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Price { get; set; }

        //cena do zapłaty
        public string FinalPrice { get; set; }

        private readonly MyDbContext _db;

        public ICollection<Product> OrderedProducts { get; set; }
        public List<Product> _orderedProducts { get; set; }

        public OrderViewModel()
        {
            _db = MyDbContext.Create();

            var user = _db.Users.Where(n => n.Name == RunTimeInfo.Instance.Username).SingleOrDefault();
            if (user != null)
            {
                FirstName = user.Name;
                Surname = user.Surname;
                StreetName = user.StreetName;
                City = user.City;
                PostalCode = user.PostalCode;
                PhoneNumber = user.PhoneNumber;
                PaymentMethod = user.PaymentMethod;
            }

            OrderedProducts = new ObservableCollection<Product>();
            _orderedProducts = new List<Product>();

            LoadData();
            CountFinalPrice();
        }

        public ICommand SaveOrderCommand
        {
            get
            {
                return new RelayCommand(p => SaveOrder());
            }
        }

        public void SaveOrder()
        {
            List<OrderProduct> op = new List<OrderProduct>();

            var o = new Order(OrderStatus.Pending, _db.Users.Where(n => n.Name == RunTimeInfo.Instance.Username).SingleOrDefault(), FirstName, Surname, StreetName, PostalCode, City, PhoneNumber, PaymentMethod, DeliveryMethod);

            foreach (Product p in OrderedProducts)
            {
                op.Add(new OrderProduct
                {
                    Product = p,
                    Order = o,
                    Quantity = 1
                });
            }

            _db.OrderProducts.AddRange(op);

            var user = _db.Users.Include(c => c.Cart).SingleOrDefault(n => n.Name == RunTimeInfo.Instance.Username);
            user.Cart.Clear();

            _db.SaveChanges();

            ApplicationViewModel.Instance.CurrentPageViewModel = ApplicationViewModel.Instance.PageViewModels.SingleOrDefault(n => n.Name == "Account");
        }

        public void CountFinalPrice()
        {
            double _finalPrice = 0.0;
            foreach(Product p in OrderedProducts)
            {
                _finalPrice += Math.Round(p.Price, 2);
            }

            FinalPrice = _finalPrice.ToString() + " zł";
        }

        private void LoadData()
        {
            var user = _db.Users.Include(c => c.Cart).SingleOrDefault(n => n.Name == RunTimeInfo.Instance.Username);

            foreach (UserCart p in user.Cart)
            {
                _orderedProducts.Add(_db.Products.Where(u => u.Id == p.ProductId).FirstOrDefault());
            }

            RefreshProducts(_orderedProducts);
        }

        private void RefreshProducts(ICollection<Product> products)
        {
            OrderedProducts.Clear();

            foreach (var item in products)
                OrderedProducts.Add(item);
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
            ApplicationViewModel.Instance.CurrentPageViewModel = new CartViewModel();
        }
    }
}
