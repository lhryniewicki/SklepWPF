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

		public string ErrorMessage
		{
			get
			{
				return _errorMessage;
			}

			set
			{
				if(_errorMessage != value)
				{

					_errorMessage = value;
					OnPropertyChanged("ErrorMessage");
				}
			}
		} 

		private ICommand _loginCommand { get; set; }
		private readonly MyDbContext _db;
		private string _errorMessage { get; set; } = "";
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
						p => Login(Username,(PasswordBox)p),
						p=>IsValid((PasswordBox)p));
				}

				return _loginCommand;

			}
		}

		private void Login( string username,  PasswordBox passwordBox)
		{
			var user = _db.Users
				.SingleOrDefault(x => x.Nickname == username &&
				x.Password == passwordBox.Password);

			if (user == null) return;
			else
			{
				RunTimeInfo.Instance.Username = username;
				ApplicationViewModel.Instance.IsUserLogged = true;
				ApplicationViewModel.Instance.CurrentPageViewModel = new ProductsViewModel();

			}

		}
		private bool IsValid(PasswordBox passwordBox)
		{
			ErrorMessage = ValidateLogin(passwordBox).ToString();
			return !string.IsNullOrEmpty(Username)
				&&!string.IsNullOrEmpty(passwordBox.Password);
		}
		private StringBuilder ValidateLogin(PasswordBox pb)
		{
			StringBuilder errorMessage = new StringBuilder();

			if (string.IsNullOrEmpty(Username)) errorMessage.Append("Pole login jest wymagane!"+ Environment.NewLine);
			if (pb == null)
			{
				errorMessage.Append("Pole hasło jest wymagane!"+ Environment.NewLine);
				return errorMessage;
			}
			
			if (string.IsNullOrEmpty(pb.Password)) errorMessage.Append("Pole hasło jest wymagane!"+Environment.NewLine);
			return errorMessage;
		}
	}
}
