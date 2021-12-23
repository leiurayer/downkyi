using System;
using System.Globalization;
using System.Windows.Data;

namespace DownKyi.Converter
{
    public class CountConverter : IValueConverter
    {
        public int Count { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)value) > Count;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
