using SklepWPF.Data;
using SklepWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace SklepWPF.ViewModels
{
	 class LoginViewModel :ObservableObject, IPageViewModel
	{
		public string Name { get; set; }
		public string Username { get; set; }

        private string wrongLoginCredentials;
        public string WrongLoginCredentials
        {
            get
            {
                return wrongLoginCredentials;
            }
            set
            {
                if (wrongLoginCredentials != value)
                    wrongLoginCredentials = value;
                OnPropertyChanged("WrongLoginCredentials");
            }
        }

		private ICommand _loginCommand { get; set; }
		private readonly MyDbContext _db;
		public LoginViewModel()
		{
			_db = MyDbContext.Create();
		}

		public ICommand loginCommand
		{

			get
			{
				if (_loginCommand == null)
				{
					_loginCommand = new RelayCommand(
						p => Login(Username,(PasswordBox)p));
				}

				return _loginCommand;

			}
		}

		private void Login( string username,  PasswordBox passwordBox)
		{
			var user = _db.Users
				.SingleOrDefault(x => x.Nickname == username &&
				x.Password == passwordBox.Password);

			if (user == null)
            {
                WrongLoginCredentials = "Zły login lub hasło";
            }

			else
			{
				RunTimeInfo.Instance.Username = username;
				ApplicationViewModel.Instance.IsUserLogged = true;
                if(user.IsAdmin)
                {
                    ApplicationViewModel.Instance.IsUserAdmin = true;
                }
				ApplicationViewModel.Instance.CurrentPageViewModel = new ProductsViewModel();

			}

		}
	}
}
