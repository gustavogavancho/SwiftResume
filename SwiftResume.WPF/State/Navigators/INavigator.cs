using SwiftResume.WPF.Core;
using System;

namespace SwiftResume.WPF.State.Navigators
{
    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        event Action StateChanged;
    }
}
