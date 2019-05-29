using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace SklepWPF
{
	[ValueConversion(typeof(bool), typeof(bool))]
	public class ReverseBooleanToVisibilityConverter : IValueConverter
	{

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if ((bool)value)
			{
				return Visibility.Collapsed;
			}
			else if(!((bool)value))
			{
				return Visibility.Visible;
			}
			else
			{
				throw new InvalidOperationException();
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return false;
		}
	}
}
