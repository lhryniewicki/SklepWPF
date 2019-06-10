using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace SklepWPF
{
    [ValueConversion(typeof(bool), typeof(string))]
    class BooleanToEnvelopeImageConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string uri = "";

            if((bool)value)
            {
                uri = "\\Images\\opened_envelope.png";
            }
            else
            {
                uri = "\\Images\\closed_envelope.png";
            }

            return uri;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
