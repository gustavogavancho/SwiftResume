using Prism.Commands;
using SwiftResume.WPF.Commands;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.State.Navigators;
using SwiftResume.WPF.ViewModels.Factories;
using SwiftResume.WPF.Views;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Application = System.Windows.Application;
using EnumLanguage = SwiftResume.COMMON.Enums;

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

        public DelegateCommand MinimizeWindowCommand { get; private set; }
        public DelegateCommand CloseWindowCommand { get; private set; }

        #endregion

        #region Construtor

        public MainViewModel(INavigator navigator,
            ISwiftResumeViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;

            _navigator.StateChanged += Navigator_StateChanged;

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory);
            UpdateCurrentViewModelCommand.Execute(EnumLanguage.ViewType.Resume);

            MinimizeWindowCommand = new DelegateCommand(OnMinimizeCommand);
            CloseWindowCommand = new DelegateCommand(OnCloseCommand);
        }

        private void OnCloseCommand()
        {
            Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().Close();
        }

        private void OnMinimizeCommand()
        {
            Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().WindowState = WindowState.Minimized;
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
