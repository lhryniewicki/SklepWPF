using SklepWPF.Data;
using SklepWPF.Enums;
using SklepWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace SklepWPF.ViewModels
{
	class ClientPanelViewModel : ObservableObject, IPageViewModel
	{
		public string Name { get; set; }

        private readonly MyDbContext _db;

        private readonly int userId;

        //Dane osobowe

        private string username;

        [Required(ErrorMessage = "Pole nie może być puste")]
        public string UserName
        {
            get
            {
                return username;
            }
            set
            {
                if(username != value)
                    username = value;
                OnPropertyChanged("UserName");
                ValidateProperty(value, "UserName");
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
                if(surname != value)
                    surname = value;
                OnPropertyChanged("Surname");
                ValidateProperty(value, "Surname");
            }
        }

        private string email;

        [Required(ErrorMessage = "Pole nie może być puste")]
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if(email != value)
                    email = value;
                OnPropertyChanged("Email");
                ValidateProperty(value, "Email");
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
                if(streetName != value)
                    streetName = value;
                OnPropertyChanged("StreetName");
                ValidateProperty(value, "StreetName");
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
                if(city != value)
                    city = value;
                OnPropertyChanged("City");
                ValidateProperty(value, "City");
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
                if(postalCode != value)
                    postalCode = value;
                OnPropertyChanged("PostalCode");
                ValidateProperty(value, "PostalCode");
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
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
                ValidateProperty(value, "PhoneNumber");
            }
        }
        
        public PaymentMethod PaymentMethod { get; set; }

        //Historia zamówień

        public ICollection<Order> Orders { get; set; }

        public ClientPanelDisplayedItems DisplayedItems { get; set; }

        public readonly int productsAndOrdersPageSize;

        private bool isNextOrdPrdPageValid;

        private int ordPrdPage;

        public string OrdPrdSearchQuery { get; set; }

        //Obserwowane produkty

        public ICollection<Product> ObservedProducts { get; set; }

        public ClientPanelViewModel()
        {
            _db = MyDbContext.Create();
            var user = _db.Users.Where(n => n.Name == RunTimeInfo.Instance.Username).SingleOrDefault();
            if(user != null)
            {
                userId = user.Id;
                UserName = user.Name;
                Surname = user.Surname;
                Email = user.Email;
                StreetName = user.StreetName;
                City = user.City;
                PostalCode = user.PostalCode;
                PhoneNumber = user.PhoneNumber;
                PaymentMethod = user.PaymentMethod;
            }

            ordPrdPage = 1;
            productsAndOrdersPageSize = 10;
            isNextOrdPrdPageValid = false;
            ObservedProducts = new ObservableCollection<Product>();
            Orders = new ObservableCollection<Order>();
            DisplayedItems = ClientPanelDisplayedItems.Orders;
            LoadOrders();
        }

        private void ValidateProperty<T>(T value, string name)
        {
            Validator.ValidateProperty(value, new ValidationContext(this, null, null)
            {
                MemberName = name
            });
        }

        public ICommand SaveUserDataCommand
        {
            get
            {
                return new RelayCommand(p => Save(),
                    p => IsFormValid());
            }
        }

        private void Save()
        {
            var user = _db.Users.Find(userId);
            user.Name = UserName;
            user.Surname = Surname;
            user.Email = Email;
            user.StreetName = StreetName;
            user.City = City;
            user.PostalCode = PostalCode;
            user.PhoneNumber = PhoneNumber;
            user.PaymentMethod = PaymentMethod;
            RunTimeInfo.Instance.Username = UserName;
            _db.SaveChanges();
        }

        private bool IsFormValid()
        {
            if(String.IsNullOrEmpty(UserName) ||
                String.IsNullOrEmpty(Surname) ||
                String.IsNullOrEmpty(Email) ||
                String.IsNullOrEmpty(StreetName) ||
                String.IsNullOrEmpty(City) ||
                String.IsNullOrEmpty(PostalCode) ||
                String.IsNullOrEmpty(PhoneNumber))
            {
                return false;
            }

            return true;
        }

        public int OrdPrdPage
        {
            get
            {
                return ordPrdPage;
            }
            set
            {
                if (ordPrdPage != value)
                    ordPrdPage = value;
                OnPropertyChanged("OrdPrdPage");
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
            return isNextOrdPrdPageValid;
        }

        private void NextPage()
        {
            OrdPrdPage++;

            if (DisplayedItems == ClientPanelDisplayedItems.Orders)
            {
                LoadOrders();
            }
            else
            {
                LoadObservedProducts();
            }
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
            return (ordPrdPage - 1) > 0;
        }

        private void PreviousPage()
        {
            OrdPrdPage--;

            if (DisplayedItems == ClientPanelDisplayedItems.Orders)
            {
                LoadOrders();
            }
            else
            {
                LoadObservedProducts();
            }
        }

        public ICommand ChangeDisplayedItemsCommand
        {
            get
            {
                return new RelayCommand(p => ChangeDisplayedItems((int)p));
            }
        }

        private void ChangeDisplayedItems(int display)
        {
            DisplayedItems = (ClientPanelDisplayedItems)display;
            OrdPrdPage = 1;
            if(display == 0)
            {
                LoadOrders();
            }
            else
            {
                LoadObservedProducts();
            }
        }

        public ICommand SearchOrdPrdCommand
        {
            get
            {
                return new RelayCommand(p => SearchOrdPrd());
            }
        }

        private void SearchOrdPrd()
        {
            OrdPrdPage = 1;
            if (DisplayedItems == ClientPanelDisplayedItems.Orders)
            {
                LoadOrders();
            }
            else
            {
                LoadObservedProducts();
            }
        }

        private void LoadOrders()
        {
            IQueryable<Order> orders = _db.Orders
                .Where(u => u.UserId == userId);

            if(!String.IsNullOrEmpty(OrdPrdSearchQuery))
            {
                orders = orders.Where(op => op.OrderedProducts.Any(p => p.Product.Name.Contains(OrdPrdSearchQuery)));
            }

            var _orders = orders.OrderBy(o => o.Created)
                .Skip((ordPrdPage - 1) * productsAndOrdersPageSize)
                .Take(productsAndOrdersPageSize + 1).ToList();

            if (_orders.Count == productsAndOrdersPageSize + 1)
            {
                isNextOrdPrdPageValid = true;
                _orders.RemoveAt(_orders.Count - 1);
            }
            else
            {
                isNextOrdPrdPageValid = false;
            }

            Orders.Clear();
            foreach(Order o in _orders)
            {
                Orders.Add(o);
            }
        }

        private void LoadObservedProducts()
        {
            IQueryable<Product> observedProducts = _db.Products
                .Where(ou => ou.ObservingUsers.Any(u => u.Id == userId));

            if (!String.IsNullOrEmpty(OrdPrdSearchQuery))
            {
                observedProducts = observedProducts.Where(n => n.Name.Contains(OrdPrdSearchQuery));
            }

            var _observedProducts = observedProducts.OrderBy(o => o.Name)
                .Skip((ordPrdPage - 1) * productsAndOrdersPageSize)
                .Take(productsAndOrdersPageSize + 1).ToList();

            if (_observedProducts.Count == productsAndOrdersPageSize + 1)
            {
                isNextOrdPrdPageValid = true;
                _observedProducts.RemoveAt(_observedProducts.Count - 1);
            }
            else
            {
                isNextOrdPrdPageValid = false;
            }

            ObservedProducts.Clear();
            foreach (Product o in _observedProducts)
            {
                ObservedProducts.Add(o);
            }
        }

        public ICommand ViewProductCommand
        {
            get
            {
                return new RelayCommand(p => ViewProduct((int)p));
            }
        }

        private void ViewProduct(int id)
        {
            ApplicationViewModel.Instance.CurrentPageViewModel = new ProductDetailsViewModel(id);
        }
    }
}
