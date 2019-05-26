using SklepWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SklepWPF.ViewModels
{
	 class ApplicationViewModel :ObservableObject
	{

		private ICommand _changePageCommand;
		private IPageViewModel _currentPageViewModel;
		public List<IPageViewModel> PageViewModels;


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
