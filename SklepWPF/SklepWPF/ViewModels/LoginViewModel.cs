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
	public class LoginViewModel : IPageViewModel
	{
		public string Name { get; set; }

		public string Username { get; set; }

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
						p => Login(Username,(PasswordBox)p),
						p=>IsValid((PasswordBox)p));
				}

				return _loginCommand;

			}
		}

		private void Login( string username,  PasswordBox passwordBox)
		{
			var user = _db.Users
				.Where(x => x.Nickname == username &&
				x.Password == passwordBox.Password)
				.SingleOrDefault();

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
			return !string.IsNullOrEmpty(Username)
				&&!string.IsNullOrEmpty(passwordBox.Password);
		}
	}
}
