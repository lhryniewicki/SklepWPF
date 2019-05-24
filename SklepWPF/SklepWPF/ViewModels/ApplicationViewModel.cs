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
		public List<IPageViewModel> PageViewModels= new List<IPageViewModel>();

		public ApplicationViewModel()
		{
			// Add available pages
			PageViewModels.Add(new ProductsViewModel { Name = "Products" });

			PageViewModels.Add(new LoginViewModel{ Name = "Login" });

			PageViewModels.Add(new RegisterViewModel { Name = "Register" });

			PageViewModels.Add(new CartViewModel { Name = "Cart" });

			PageViewModels.Add(new ClientPanelViewModel { Name = "Account" });



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
