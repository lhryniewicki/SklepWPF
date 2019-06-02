using SklepWPF.Data;
using SklepWPF.Enums;
using SklepWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Data.Entity;

namespace SklepWPF.ViewModels
{
	class ClientPanelViewModel : ObservableObject, IPageViewModel
	{
		public string Name { get; set; }

        private readonly MyDbContext _db;

        private readonly int UserId;

        //Dane osobowe

        public string UserName { get; set; }
        
        public string Surname { get; set; }
        
        public string Email { get; set; }
        
        public string StreetName { get; set; }
        
        public string City { get; set; }
        
        public string PostalCode { get; set; }

        public string PhoneNumber { get; set; }
        
        public PaymentMethod PaymentMethod { get; set; }

        //Wiadomości

        public ICollection<Message> Messages { get; set; }

        public MessageDisplay DisplayedMessages { get; set; }

        private readonly int messagePageSize;

        private bool IsNextMsgPageValid; 

        private int messagePage;

        public string MessageSearchQuery { get; set; }

        //Historia zamówień

        public ICollection<Order> Orders { get; set; }

        public ClientPanelDisplayedItems DisplayedItems { get; set; }

        public readonly int productsAndOrdersPageSize;

        private bool IsNextOrdPrdPageValid;

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
                UserId = user.Id;
                UserName = user.Name;
                Surname = user.Surname;
                Email = user.Email;
                StreetName = user.StreetName;
                City = user.City;
                PostalCode = user.PostalCode;
                PhoneNumber = user.PhoneNumber;
                PaymentMethod = user.PaymentMethod;
            }
            messagePage = 1;
            messagePageSize = 4;
            ordPrdPage = 1;
            productsAndOrdersPageSize = 10;
            IsNextMsgPageValid = false;
            Messages = new ObservableCollection<Message>();
            ObservedProducts = new ObservableCollection<Product>();
            Orders = new ObservableCollection<Order>();
            DisplayedMessages = MessageDisplay.Received;
            DisplayedItems = ClientPanelDisplayedItems.Orders;
            LoadMessages();
            LoadOrders();
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
            var user = _db.Users.Find(UserId);
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

        public int MessagePage
        {
            get
            {
                return messagePage;
            }
            set
            {
                if (messagePage != value)
                    messagePage = value;
                OnPropertyChanged("MessagePage");
            }
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
                return new RelayCommand(p => NextPage((string)p),
                    p => IsValidNext((string)p));
            }
        }

        private bool IsValidNext(string collectionName)
        {
            if (collectionName == "Messages")
            {
                return IsNextMsgPageValid;
            }
            else if (collectionName == "OrdersAndProducts")
            {
                return IsNextOrdPrdPageValid;
            }
            return false;
        }

        private void NextPage(string collectionName)
        {
            if (collectionName == "Messages")
            {
                MessagePage++;
                LoadMessages();
            }
            else if (collectionName == "OrdersAndProducts")
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
        }

        public ICommand PreviousPageCommand
        {
            get
            {
                return new RelayCommand(p => PreviousPage((string)p),
                    p => IsValidPrevious((string)p));
            }
        }

        private bool IsValidPrevious(string collectionName)
        {
            if (collectionName == "Messages")
            {
                return (messagePage - 1) > 0;
            }
            else if (collectionName == "OrdersAndProducts")
            {
                return (ordPrdPage - 1) > 0;
            }
            return false;
        }

        private void PreviousPage(string collectionName)
        {
            if (collectionName == "Messages")
            {
                MessagePage--;
                LoadMessages();
            }
            else if (collectionName == "OrdersAndProducts")
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
        }

        public ICommand ChangeMessagesCommand
        {
            get
            {
                return new RelayCommand(p => ChangeMessages((int)p));
            }
        }

        private void ChangeMessages(int display)
        {
            DisplayedMessages = (MessageDisplay)display;
            MessagePage = 1;
            LoadMessages();
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

        public ICommand SearchMessagesCommand
        {
            get
            {
                return new RelayCommand(p => SearchMessages());
            }
        }

        private void SearchMessages()
        {
            MessagePage = 1;
            LoadMessages();
        }

        public ICommand DeleteSelectedMessagesCommand
        {
            get
            {
                return new RelayCommand(p => DeleteSelectedMessages(((System.Collections.IList)p).Cast<Message>().ToList()));
            }
        }

        private void DeleteSelectedMessages(ICollection<Message> selectedMessages)
        {
            foreach(Message m in selectedMessages)
            {
                if (DisplayedMessages == MessageDisplay.Sent)
                {
                    if (m.ReceiverId == null)
                    {
                        _db.Messages.Remove(m);
                    }
                    else
                    {
                        m.Author = null;
                    }
                }
                else
                {
                    if (m.AuthorId == null)
                    {
                        _db.Messages.Remove(m);
                    }
                    else
                    {
                        m.Receiver = null;
                    }
                }

                Messages.Remove(m);
            }

            _db.SaveChanges();

            LoadMessages();
        }

        public ICommand ClearMessageBoxCommand
        {
            get
            {
                return new RelayCommand(p => ClearMessageBox());
            }
        }

        private void ClearMessageBox()
        {
            ICollection<Message> messages;
            List<Message> dbRemovedMessages = new List<Message>();

            var user = _db.Users.Where(n => n.Name == RunTimeInfo.Instance.Username);

            if (DisplayedMessages == MessageDisplay.Sent)
            {
                messages = user.Include(m => m.SentMessages).SingleOrDefault().SentMessages;
            }
            else
            {
                messages = user.Include(m => m.SentMessages).SingleOrDefault().ReceivedMessages;
            }

            if(messages != null)
            {
                foreach (Message m in messages)
                {
                    if (m.AuthorId == null || m.ReceiverId == null)
                    {
                        dbRemovedMessages.Add(m);
                    }
                }

                messages.Clear();
                _db.Messages.RemoveRange(dbRemovedMessages);
                _db.SaveChanges();

                MessagePage = 1;
                Messages.Clear();
                IsNextMsgPageValid = false;
            }
        }

        private void LoadMessages()
        {
            IQueryable<Message> messages;

            if (DisplayedMessages == MessageDisplay.Sent)
            {
                messages = _db.Messages.Where(m => m.AuthorId == UserId);
            }
            else
            {
                messages = _db.Messages.Where(m => m.ReceiverId == UserId);
            }
           
            if(!String.IsNullOrEmpty(MessageSearchQuery))
            {
                messages = messages.Where(m => m.Title.Contains(MessageSearchQuery));
            }

            var _messages = messages.OrderBy(d => d.Created).Skip((messagePage - 1) * messagePageSize)
                    .Take(messagePageSize + 1).ToList();

            if (_messages.Count == messagePageSize + 1)
            {
                IsNextMsgPageValid = true;
                _messages.RemoveAt(_messages.Count - 1);
            }
            else
            {
                IsNextMsgPageValid = false;
            }

            Messages.Clear();
            for(int i = 0; i < _messages.Count; i++)
            {
                Messages.Add(_messages[i]);
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
                .Where(u => u.UserId == UserId);

            if(!String.IsNullOrEmpty(OrdPrdSearchQuery))
            {
                orders = orders.Where(op => op.OrderedProducts.Any(p => p.Product.Name.Contains(OrdPrdSearchQuery)));
            }

            orders = orders.OrderBy(o => o.Created)
                .Skip((ordPrdPage - 1) * productsAndOrdersPageSize)
                .Take(productsAndOrdersPageSize);

            Orders.Clear();
            foreach(Order o in orders)
            {
                Orders.Add(o);
            }
        }

        private void LoadObservedProducts()
        {
            IQueryable<Product> observedProducts = _db.Products
                .Where(ou => ou.ObservingUsers.Any(u => u.Id == UserId));

            if (!String.IsNullOrEmpty(OrdPrdSearchQuery))
            {
                observedProducts = observedProducts.Where(n => n.Name.Contains(OrdPrdSearchQuery));
            }

            observedProducts = observedProducts.OrderBy(o => o.Name)
                .Skip((ordPrdPage - 1) * productsAndOrdersPageSize)
                .Take(productsAndOrdersPageSize);

            ObservedProducts.Clear();
            foreach (Product o in observedProducts)
            {
                ObservedProducts.Add(o);
            }
        }
    }
}
