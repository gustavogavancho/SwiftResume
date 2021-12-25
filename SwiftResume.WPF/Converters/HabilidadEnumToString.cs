using SwiftResume.COMMON.Enums;
using System.Globalization;
using System.Windows.Data;

namespace SwiftResume.WPF.Converters;

public class HabilidadEnumToString : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) return null;

        TipoHabilidad habilidad = (TipoHabilidad)Enum.Parse(typeof(TipoHabilidad), value.ToString());
        return habilidad;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}