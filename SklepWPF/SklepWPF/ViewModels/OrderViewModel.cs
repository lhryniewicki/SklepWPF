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
using System.ComponentModel.DataAnnotations;

namespace SklepWPF.ViewModels
{
    class OrderViewModel : ObservableObject, IPageViewModel
    {
        //dane osobowe użytkownika
        private string firstName;
        [Required(ErrorMessage = "Pole nie może być puste")]
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (firstName != value)
                    firstName = value;
                ValidateProperty(value, "FirstName");
            }
        }

        private string surname;
        [Required(ErrorMessage = "Pole nie może być puste")]
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                if (surname != value)
                    surname = value;
                ValidateProperty(value, "Surname");
            }
        }

        private string streetName;
        [Required(ErrorMessage = "Pole nie może być puste")]
        public string StreetName
        {
            get
            {
                return streetName;
            }
            set
            {
                if (streetName != value)
                    streetName = value;
                ValidateProperty(value, "StreetName");
            }
        }

        private string postalCode;
        [Required(ErrorMessage = "Pole nie może być puste")]
        public string PostalCode
        {
            get
            {
                return postalCode;
            }
            set
            {
                if (postalCode != value)
                    postalCode = value;
                ValidateProperty(value, "PostalCode");
            }
        }

        private string city;
        [Required(ErrorMessage = "Pole nie może być puste")]
        public string City
        {
            get
            {
                return city;
            }
            set
            {
                if (city != value)
                    city = value;
                ValidateProperty(value, "City");
            }
        }

        private string phoneNumber;
        [Required(ErrorMessage = "Pole nie może być puste")]
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                if (phoneNumber != value)
                    phoneNumber = value;
                ValidateProperty(value, "PhoneNumber");
            }
        }

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

        public bool IsValid()
        {
            if (String.IsNullOrEmpty(FirstName) ||
               String.IsNullOrEmpty(Surname) ||
               String.IsNullOrEmpty(StreetName) ||
               String.IsNullOrEmpty(PostalCode) ||
               String.IsNullOrEmpty(City) ||
               String.IsNullOrEmpty(PhoneNumber))

                return false;

            return true;
        }

        public void SaveOrder()
        {
            List<OrderProduct> op = new List<OrderProduct>();

            if (!IsValid())
                return;

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

        private void ValidateProperty<T>(T value, string name)
        {
            Validator.ValidateProperty(value, new ValidationContext(this, null, null)
            {
                MemberName = name
            });
        }
    }
}
