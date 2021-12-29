using System.Globalization;
using System.Windows.Data;

namespace SwiftResume.WPF.Converters;

public class EsActualToString : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((bool)value) return "Actual";
        else return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}