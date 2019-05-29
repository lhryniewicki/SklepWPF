using SklepWPF.ViewModels;
using SklepWPF.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace SklepWPF
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			ApplicationView app = new ApplicationView();
			ApplicationViewModel context = ApplicationViewModel.Instance;
			app.DataContext = context;
			app.Show();
		}
	}
}
