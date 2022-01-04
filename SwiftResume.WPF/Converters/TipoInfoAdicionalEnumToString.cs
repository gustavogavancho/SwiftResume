﻿using SwiftResume.COMMON.Enums;
using System.Globalization;
using System.Windows.Data;

namespace SwiftResume.WPF.Converters
{
    public class TipoInfoAdicionalEnumToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            TipoInfoAdicional tipoInfoAdicional = (TipoInfoAdicional)Enum.Parse(typeof(TipoInfoAdicional), value.ToString());
            return tipoInfoAdicional;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
