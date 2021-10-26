namespace Giessformkonfigurator.WPF.MVVM.View.Converter
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;

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
