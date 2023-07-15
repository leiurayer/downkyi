using Avalonia;
using Avalonia.Data.Converters;
using Downkyi.Services;
using System;
using System.Globalization;

namespace Downkyi.Converter;

/// <summary>
/// 通过字符串访问Resource资源
/// </summary>
public class StringToResourceConverter : IValueConverter
{
    public static StringToResourceConverter Instance { get; } = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null)
            return null;

        return DictionaryResource.GetResource(Application.Current?.Resources, value.ToString()!);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}