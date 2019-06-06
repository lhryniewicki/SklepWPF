using SklepWPF.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SklepWPF.Models
{
	public class User
	{
		public int Id { get; set; }
		[Required]
		public string Nickname { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Surname { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string StreetName { get; set; }
		[Required]
		public string City { get; set; }
		[Required]
		public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }
        public ICollection<Order> Orders { get; set; }
		public ICollection<UserCart> Cart { get; set; }
        public ICollection<Product> ObservedProducts { get; set; }

		public User()
		{
		}

		public User(string nickname, string username, string surname, string streetName, string city, string postalCode, string password, string email)
		{
			Nickname = nickname;
			Name = username;
			Surname = surname;
			StreetName = streetName;
			City = city;
			PostalCode = postalCode;
			Password = password;
			Email = email;
		}

	}
}
