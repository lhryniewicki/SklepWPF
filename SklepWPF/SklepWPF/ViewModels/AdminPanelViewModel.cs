using SklepWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SklepWPF.ViewModels
{
    class AdminPanelViewModel : IPageViewModel
    {
        public string Name { get; set; }

        public ICommand EditProductsCommand
        {
            get
            {
                return new RelayCommand(p => EditProducts());
            }
        }

        public void EditProducts()
        {
            ApplicationViewModel.Instance.CurrentPageViewModel = new ProductListViewModel();
        }

        public ICommand UserListCommand
        {
            get
            {
                return new RelayCommand(p => UserList());
            }
        }

        public void UserList()
        {
            ApplicationViewModel.Instance.CurrentPageViewModel = new ClientListViewModel();
        }
    }
}
