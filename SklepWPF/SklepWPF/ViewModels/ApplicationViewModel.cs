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
		private IPageViewModel _currentPageViewModel;
		public List<IPageViewModel> PageViewModels;

		private bool _isUserLogged = false;

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

				new CartViewModel { Name = "Cart" },

				new ClientPanelViewModel { Name = "Account" }
			};


			// Set starting page
			CurrentPageViewModel = PageViewModels[0];
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


		private void ChangeViewModel(string name)
		{
			CurrentPageViewModel = PageViewModels
				.Where(x => x.Name == name)
				.SingleOrDefault();
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
