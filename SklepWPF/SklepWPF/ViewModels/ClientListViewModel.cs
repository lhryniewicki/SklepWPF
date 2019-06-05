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
    class ClientListViewModel : ObservableObject, IPageViewModel
    {
        //dane użytkowników
        public string Nickname { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }

        private readonly MyDbContext _db;

        public ICollection<User> Users { get; set; }
        public List<User> _users { get; set; }

        private readonly int _pageSize;
        private int _productsQuantity;
        private int _page;

        public ClientListViewModel()
        {
            _db = MyDbContext.Create();

            Users = new ObservableCollection<User>();
            _users = new List<User>();

            _pageSize = 25;
            Page = 1;

            LoadData();
        }

        public ICommand DisplayUserListCommand
        {
            get
            {
                return new RelayCommand(p => DisplayUserList());
            }
        }

        public void DisplayUserList()
        {

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
            RefreshProducts(_users);
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
            RefreshProducts(_users);

        }

        private void LoadData()
        {
            _users = _db.Users.ToList();
            RefreshProducts(_users);
        }

        private void RefreshProducts(ICollection<User> users)
        {
            Users.Clear();

            _productsQuantity = users.Count;
            var loadUsers = users.Skip(_pageSize * (Page - 1)).Take(_pageSize);

            foreach (var item in loadUsers) Users.Add(item);
        }
    }
}
