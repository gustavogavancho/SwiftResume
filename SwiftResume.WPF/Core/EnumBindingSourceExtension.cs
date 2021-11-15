using System.Windows.Markup;

namespace SwiftResume.WPF.Core;

public class EnumBindingSourceExtension : MarkupExtension
{
    public Type EnumType { get; private set; }

    public EnumBindingSourceExtension(Type enumType)
    {
        if (enumType is null || !enumType.IsEnum)
            return;

        EnumType = enumType;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Enum.GetValues(EnumType);
    }
}

