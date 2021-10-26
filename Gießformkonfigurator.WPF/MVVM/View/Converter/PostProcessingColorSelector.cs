namespace Giessformkonfigurator.WPF.MVVM.View.Converter
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    public class PostProcessingColorSelector : IValueConverter
    {
        private readonly Color negativeValueColor = Colors.Red;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                // Currently not in use.
                // return dec < 0 ? new SolidColorBrush(this.negativeValueColor) : Binding.DoNothing;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
