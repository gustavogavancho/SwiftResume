using SwiftResume.COMMON.Util;
using System.Globalization;
using System.Windows.Data;

namespace SwiftResume.WPF.Converters;

public class SplitCamelCaseString : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) return null;

        string valueString = value.ToString().SplitCamelCase();

        return valueString.Substring(0, 1) + valueString.Substring(1).ToLower();

    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}