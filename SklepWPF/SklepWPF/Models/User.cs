﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SklepWPF.Models
{
	class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public Adress Adress { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }

	}
}
