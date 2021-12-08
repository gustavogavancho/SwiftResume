using SwiftResume.WPF.Core;

namespace SwiftResume.WPF.CustomControls.Tab;

public abstract class Tab : ViewModelBase,  ITab
{
    public string Name { get; set; }
}