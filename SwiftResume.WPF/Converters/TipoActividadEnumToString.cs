using SwiftResume.COMMON.Enums;
using System.Globalization;
using System.Windows.Data;

namespace SwiftResume.WPF.Converters;

public class TipoActividadEnumToString : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) return null;

        TipoActividad tipoEducacion = (TipoActividad)Enum.Parse(typeof(TipoActividad), value.ToString());
        return tipoEducacion;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value.ToString();
    }
}