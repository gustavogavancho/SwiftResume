using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SwiftResume.COMMON.Util;

public static class Util
{
    public static string GetEnumDescription(Enum enumObj)
    {
        if (enumObj == null)
        {
            return string.Empty;
        }
        FieldInfo fieldInfo = enumObj.GetType().GetField(enumObj.ToString());

        object[] attribArray = fieldInfo?.GetCustomAttributes(false);

        if (attribArray?.Length == 0)
        {
            return enumObj.ToString();
        }
        else
        {
            DescriptionAttribute attrib = attribArray?[0] as DescriptionAttribute;
            return attrib?.Description;
        }
    }

    public static string SplitCamelCase(this string str)
    {
        return Regex.Replace(
            Regex.Replace(
                str,
                @"(\P{Ll})(\P{Ll}\p{Ll})",
                "$1 $2"
            ),
            @"(\p{Ll})(\P{Ll})",
            "$1 $2"
        );
    }
}