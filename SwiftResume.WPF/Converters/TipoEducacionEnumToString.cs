using SwiftResume.COMMON.Enums;
using SwiftResume.COMMON.Util;
using System.Globalization;
using System.Windows.Data;

namespace SwiftResume.WPF.Converters;

public class TipoEducacionEnumToString : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) return null;

        TipoEducacion tipoEducacion = (TipoEducacion)Enum.Parse(typeof(TipoEducacion), value.ToString());
        return tipoEducacion;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value.ToString();
    }
}