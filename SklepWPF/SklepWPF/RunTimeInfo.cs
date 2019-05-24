using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SklepWPF
{
	public class RunTimeInfo :DependencyObject
	{
		public  string Username
		{
			get { return (string)GetValue(UsernameProperty); }
			set { SetValue(UsernameProperty, value); }
		}

		public static RunTimeInfo Instance {get;private set;}

		static RunTimeInfo()
		{
			Instance = new RunTimeInfo();
		}

		public static readonly DependencyProperty UsernameProperty =
	  DependencyProperty.Register("Username", typeof(string),
	  typeof(RunTimeInfo), new UIPropertyMetadata((null)));
	}
}
