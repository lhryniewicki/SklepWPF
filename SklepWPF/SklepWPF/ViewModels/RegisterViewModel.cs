using SklepWPF.Data;
using SklepWPF.Models;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace SklepWPF.ViewModels
{
	class RegisterViewModel : IPageViewModel
	{
		public string Name { get; set; }//Nazwa viewmodelu

		public string Nickname { get; set; }

		public string UserName { get; set; }
		
		public string Surname { get; set; }

		public string StreetName { get; set; }
		
		public string City { get; set; }
		
		public string PostalCode { get; set; }

		public string Email { get; set; }

		private readonly MyDbContext _db;

		private ICommand _RegisterUserCommand { get; set; }


		public RegisterViewModel()
		{
			_db = MyDbContext.Create();
		}

		public ICommand RegisterUserCommand
		{
			get
			{
				if (_RegisterUserCommand == null)
				{
					_RegisterUserCommand = new RelayCommand
						(p => RegisterUser(Nickname, UserName, Surname, StreetName, City, PostalCode, Email, (PasswordBox)p	),
						p => IsValid((PasswordBox)p));
				}

				return _RegisterUserCommand;

			}
		}

		public void RegisterUser(string nickname, string username, string surname, string streetName, string city,string  postalCode, string email, PasswordBox password)
		{
			var newUser = new User(nickname,username,surname,streetName,city,postalCode,password.Password,email);
			
			_db.Users.Add(newUser);
			_db.SaveChanges();
			ApplicationViewModel.Instance.CurrentPageViewModel = new LoginViewModel();
		}

		private bool IsValid(PasswordBox passwordBox)
		{
			var isNicknameTaken = _db.Users
				.Where(x => x.Nickname == Nickname)
				.SingleOrDefault();

			if (isNicknameTaken != null) return false;

			return !string.IsNullOrEmpty(Nickname) &&
				!string.IsNullOrEmpty(UserName) &&
				!string.IsNullOrEmpty(Surname) &&
				!string.IsNullOrEmpty(StreetName) &&
				!string.IsNullOrEmpty(City) && 
				!string.IsNullOrEmpty(passwordBox.Password) &&
				!string.IsNullOrEmpty(PostalCode) &&
				!string.IsNullOrEmpty(Email) 
				;
						

		}


	}
}
