using SwiftResume.COMMON.Enums;
using SwiftResume.WPF.State.Navigators;
using SwiftResume.WPF.ViewModels.Factories;

namespace SwiftResume.WPF.Commands;

public class UpdateCurrentViewModelCommand : ICommand
{
    public event EventHandler CanExecuteChanged;

    private readonly INavigator _navigator;
    private readonly ISwiftResumeViewModelFactory _viewModelFactory;

    public UpdateCurrentViewModelCommand(INavigator navigator, ISwiftResumeViewModelFactory viewModelFactory)
    {
        _navigator = navigator;
        _viewModelFactory = viewModelFactory;
    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        if (parameter is ViewType viewType)
        {
            _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
        }
    }
}
