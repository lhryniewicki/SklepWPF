using SklepWPF.Data;
using SklepWPF.ViewModels;
using SklepWPF.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;

namespace SklepWPF
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			var db = MyDbContext.Create();
			var _destinationPath = AppDomain.CurrentDomain.BaseDirectory + "Images\\";
			_destinationPath = _destinationPath.Replace("\\", "/");
			_destinationPath = _destinationPath.Replace("/bin/Debug", "");
			var imagePath = db.Products.Select(x=>x.ImagePath).FirstOrDefault();

			if(!_destinationPath.Contains(imagePath))
			{

				foreach (var overwritePath in db.Products)
				overwritePath.ImagePath = _destinationPath + Path.GetFileName(overwritePath.ImagePath);

				db.SaveChanges();

			}
			

			base.OnStartup(e);
            ApplicationView app = new ApplicationView();
            ApplicationViewModel context = ApplicationViewModel.Instance;
            app.DataContext = context;
            app.Show();
        }
    }
}
