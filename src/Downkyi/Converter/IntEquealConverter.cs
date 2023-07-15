using Avalonia.Data;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace Downkyi.Converter;

public class IntEquealConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (parameter is string str)
        {
            if (value is int source && int.TryParse(str, out int target))
            {
                return source == target;
            }
        }
        else if (parameter is int integer)
        {
            if (value is int source && targetType.IsAssignableTo(typeof(bool)))
            {
                return source == integer;
            }
        }

        return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}