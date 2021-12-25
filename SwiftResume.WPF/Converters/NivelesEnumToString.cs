using SwiftResume.COMMON.Enums;
using System.Globalization;
using System.Windows.Data;

namespace SwiftResume.WPF.Converters;

public class NivelesEnumToString : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) return null;

        Niveles language = (Niveles)Enum.Parse(typeof(Niveles), value?.ToString());
        return language;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}