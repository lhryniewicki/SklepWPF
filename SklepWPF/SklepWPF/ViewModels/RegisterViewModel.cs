using SklepWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SklepWPF.ViewModels
{
	class RegisterViewModel : IPageViewModel
	{
		public string Name { get; set; }

		public string UserName { get; set; }
		
		public string Surname { get; set; }
	
		public Adress Adress { get; set; }

		public string Password { get; set; }

		public string ConfirmPassword { get; set; }

		public string Email { get; set; }
	}
}
