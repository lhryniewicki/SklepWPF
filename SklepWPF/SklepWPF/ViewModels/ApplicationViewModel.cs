using SklepWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

//TO DO 
//	NAPRAWIC POJAWIANIE SIE NAWIGACJI PO LOGOWANIU
namespace SklepWPF.ViewModels
{
	 class ApplicationViewModel :ObservableObject
	{

		private static ApplicationViewModel _instance = null;

		private ICommand _changePageCommand;
		private ICommand _logOutCommand;

		private IPageViewModel _currentPageViewModel;
		public List<IPageViewModel> PageViewModels;

		private bool _isUserLogged = false;
        private bool _isUserAdmin = false;

		public static ApplicationViewModel Instance
		{
			get
			{
				if (_instance == null) _instance = new ApplicationViewModel();
				return _instance;
			}
		}

		public ApplicationViewModel()
		{
            PageViewModels = new List<IPageViewModel>
            {
				// Add available pages
				new ProductsViewModel { Name = "Products" },

                new LoginViewModel { Name = "Login" },

                new RegisterViewModel { Name = "Register" },

                new ClientPanelViewModel { Name = "Account" },

                new MessageBoxViewModel { Name = "MessageBox" },

                new AdminPanelViewModel { Name = "AdminPanel" },
            };


			// Set starting page
			CurrentPageViewModel = PageViewModels[0];
		}

        public ICommand CartCommand
        {
            get
            {
                return new RelayCommand(p => Cart());
            }
        }

        public void Cart()
        {
            ApplicationViewModel.Instance.CurrentPageViewModel = new CartViewModel();
        }

        public ICommand LogOutCommand
		{
			get
			{
				if (_logOutCommand == null)
				{
					_logOutCommand = new RelayCommand(p => LogOut(),
						p=>IsLogOutValid());
				}

				return _logOutCommand;

			}
		}

		private bool IsLogOutValid()
		{
			if (RunTimeInfo.Instance.Username != "Konto") return true;
			return false;
		}

		private void LogOut()
		{

			RunTimeInfo.Instance.Username = "Konto";
			IsUserLogged = false;
            IsUserAdmin = false;
			CurrentPageViewModel = new LoginViewModel();
		}

		public ICommand ChangePageCommand
		{
			get
			{
				if (_changePageCommand == null)
				{
					_changePageCommand = new RelayCommand(
						p => ChangeViewModel((string)p));
				}

				return _changePageCommand;

			}
		}
	
		private void ChangeViewModel(string name)
		{
			CurrentPageViewModel = PageViewModels
				.SingleOrDefault(x => x.Name == name);
		}

        public bool IsUserAdmin
        {
            get
            {
                return _isUserAdmin;
            }
            set
            {
                if (_isUserAdmin != value)
                {

                    _isUserAdmin = value;
                    OnPropertyChanged("IsUserAdmin");
                }
            }
        }

        public bool IsUserLogged
		{
			get
			{
				return _isUserLogged;
			}
			set
			{
				if(_isUserLogged != value)
				{

					_isUserLogged = value;
					OnPropertyChanged("IsUserLogged");
				}
			}
		}

		public IPageViewModel CurrentPageViewModel
		{
			get
			{
				return _currentPageViewModel;
			}
			set
			{
				if (_currentPageViewModel != value)
				{
					_currentPageViewModel = value;
					OnPropertyChanged("CurrentPageViewModel");
				}
			}
		}
	}
}
