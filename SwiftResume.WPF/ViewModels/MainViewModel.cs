using SwiftResume.COMMON.Enums;
using SwiftResume.WPF.Commands;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.State.Navigators;
using SwiftResume.WPF.ViewModels.Factories;
using System.Windows.Input;

namespace SwiftResume.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private readonly INavigator _navigator;
        private readonly ISwiftResumeViewModelFactory _viewModelFactory;

        #endregion

        #region Properties

        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;
        public INavigator Navigator { get; set; }
        public ICommand UpdateCurrentViewModelCommand { get; }
        public string Language { get; set; }

        #endregion

        #region Commands

        public ICommand MinimizeWindowCommand { get; private set; }
        public ICommand CloseWindowCommand { get; set; }

        #endregion

        #region Construtor

        public MainViewModel(INavigator navigator,
            ISwiftResumeViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;

            _navigator.StateChanged += Navigator_StateChanged;

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory);
            UpdateCurrentViewModelCommand.Execute(ViewType.Resume);

            MinimizeWindowCommand = new MinimizeWindowCommand();
            CloseWindowCommand = new CloseWindowCommand();
        }

        #endregion

        #region Methods
        public override void Dispose()
        {
            _navigator.StateChanged -= Navigator_StateChanged;

            base.Dispose();
        }

        private void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        #endregion
    }
}
