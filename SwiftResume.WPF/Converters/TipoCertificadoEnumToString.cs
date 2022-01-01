using SwiftResume.COMMON.Enums;
using System.Globalization;
using System.Windows.Data;

namespace SwiftResume.WPF.Converters;

public class TipoCertificadoEnumToString : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) return null;

        TipoCertificado tipoEducacion = (TipoCertificado)Enum.Parse(typeof(TipoCertificado), value.ToString());
        return tipoEducacion;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value.ToString();
    }
}