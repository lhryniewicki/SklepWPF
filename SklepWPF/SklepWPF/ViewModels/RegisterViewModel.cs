using SklepWPF.Data;
using SklepWPF.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace SklepWPF.ViewModels
{
	class RegisterViewModel : ObservableObject, IPageViewModel
	{
		public string Name { get; set; }//Nazwa viewmodelu

        private string nickname;
        [Required(ErrorMessage = "Pole nie może być puste")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "Nazwa użytkownika musi mieć co najmniej 5 znaków")]
        public string Nickname
        {
            get
            {
                return nickname;
            }
            set
            {
                if (nickname != value)
                    nickname = value;
                OnPropertyChanged("Nickname");
                ValidateProperty(value, "Nickname");
            }
        }

        private string username;

        [Required(ErrorMessage = "Pole nie może być puste")]
        public string UserName
        {
            get
            {
                return username;
            }
            set
            {
                if (username != value)
                    username = value;
                OnPropertyChanged("UserName");
                ValidateProperty(value, "UserName");
            }
        }

        private string surname;

        [Required(ErrorMessage = "Pole nie może być puste")]
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                if (surname != value)
                    surname = value;
                OnPropertyChanged("Surname");
                ValidateProperty(value, "Surname");
            }
        }

        private string email;

        [Required(ErrorMessage = "Pole nie może być puste")]
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (email != value)
                    email = value;
                OnPropertyChanged("Email");
                ValidateProperty(value, "Email");
            }
        }

        private string streetName;

        [Required(ErrorMessage = "Pole nie może być puste")]
        public string StreetName
        {
            get
            {
                return streetName;
            }
            set
            {
                if (streetName != value)
                    streetName = value;
                OnPropertyChanged("StreetName");
                ValidateProperty(value, "StreetName");
            }
        }

        private string city;

        [Required(ErrorMessage = "Pole nie może być puste")]
        public string City
        {
            get
            {
                return city;
            }
            set
            {
                if (city != value)
                    city = value;
                OnPropertyChanged("City");
                ValidateProperty(value, "City");
            }
        }

        private string postalCode;

        [Required(ErrorMessage = "Pole nie może być puste")]
        public string PostalCode
        {
            get
            {
                return postalCode;
            }
            set
            {
                if (postalCode != value)
                    postalCode = value;
                OnPropertyChanged("PostalCode");
                ValidateProperty(value, "PostalCode");
            }
        }

        private string phoneNumber;

        [Required(ErrorMessage = "Pole nie może być puste")]
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
                ValidateProperty(value, "PhoneNumber");
            }
        }

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
            if (String.IsNullOrEmpty(Nickname) ||
                String.IsNullOrEmpty(UserName) ||
                String.IsNullOrEmpty(Surname) ||
                String.IsNullOrEmpty(Email) ||
                String.IsNullOrEmpty(StreetName) ||
                String.IsNullOrEmpty(City) ||
                String.IsNullOrEmpty(PostalCode) ||
                String.IsNullOrEmpty(passwordBox.Password) ||
                String.IsNullOrEmpty(PhoneNumber))
            {
                return false;
            }

            else
            {
                if(Nickname.Length < 5)
                {
                    return false;
                }

                return true;
            }
        }

        private void ValidateProperty<T>(T value, string name)
        {
            Validator.ValidateProperty(value, new ValidationContext(this, null, null)
            {
                MemberName = name
            });
        }
    }
}
