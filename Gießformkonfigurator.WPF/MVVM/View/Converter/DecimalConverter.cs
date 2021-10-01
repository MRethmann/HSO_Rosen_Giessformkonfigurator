using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Gießformkonfigurator.WPF.MVVM.View.Converter
{
    public class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal data;
            Decimal.TryParse(value?.ToString(), out data);

            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal data;
            Decimal.TryParse(value?.ToString(), out data);

            return data;
        }
    }
}
