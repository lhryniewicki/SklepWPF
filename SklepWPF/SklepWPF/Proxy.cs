using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SklepWPF
{
	
		public class Proxy : Freezable
		{
			protected override Freezable CreateInstanceCore()
			{
				return new Proxy();
			}

			public object Data
			{
				get { return GetValue(DataProperty); }
				set { SetValue(DataProperty, value); }
			}

			public static readonly DependencyProperty DataProperty =
				DependencyProperty.Register("Data", typeof(object), typeof(Proxy), new UIPropertyMetadata(null));
		}
	
}
