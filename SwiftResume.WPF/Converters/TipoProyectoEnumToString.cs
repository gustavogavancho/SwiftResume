using SwiftResume.COMMON.Enums;
using System.Globalization;
using System.Windows.Data;

namespace SwiftResume.WPF.Converters;

public class TipoProyectoEnumToString : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) return null;

        TipoProyecto tipoProyecto = (TipoProyecto)Enum.Parse(typeof(TipoProyecto), value.ToString());
        return tipoProyecto;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}