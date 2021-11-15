using SwiftResume.WPF.Core;

namespace SwiftResume.WPF.State.Navigators;

public interface INavigator
{
    ViewModelBase CurrentViewModel { get; set; }
    event Action StateChanged;
}