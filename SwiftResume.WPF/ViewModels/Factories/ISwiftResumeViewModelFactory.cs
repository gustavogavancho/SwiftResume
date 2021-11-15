using SwiftResume.COMMON.Enums;
using SwiftResume.WPF.Core;

namespace SwiftResume.WPF.ViewModels.Factories;

public interface ISwiftResumeViewModelFactory
{
    ViewModelBase CreateViewModel(ViewType viewType);
}