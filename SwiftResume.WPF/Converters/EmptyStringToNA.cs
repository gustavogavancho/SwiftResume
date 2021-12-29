using System.Globalization;
using System.Windows.Data;

namespace SwiftResume.WPF.Converters;

public class EmptyStringToNA : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string valueToString = value.ToString();
        if (string.IsNullOrEmpty(valueToString))
            return "- NA";
        else
            return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}