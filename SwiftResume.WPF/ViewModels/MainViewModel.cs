using Prism.Commands;
using SwiftResume.WPF.Commands;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.State.Authenticators;
using SwiftResume.WPF.State.Navigators;
using SwiftResume.WPF.ViewModels.Factories;
using SwiftResume.WPF.Views;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using EnumLanguage = SwiftResume.COMMON.Enums;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.CustomControls.Dialogs.YesNo;
using SwiftResume.COMMON.Enums;
using Application = System.Windows.Application;

namespace SwiftResume.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private readonly INavigator _navigator;
        private readonly ISwiftResumeViewModelFactory _viewModelFactory;
        private readonly IAuthenticator _authenticator;
        private readonly ViewModelDelegateRenavigator<LoginViewModel> _viewModelDelegateRenavigator;
        private readonly IDialogService _dialogService;
        private readonly YesNoDialogViewModel _yesNoDialogViewModel;

        #endregion

        #region Properties

        public bool IsLoggedIn => _authenticator.IsLoggedIn;
        public string Username => $"Bienvenido {_authenticator.CurrentUser?.Username}!";

        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;
        public INavigator Navigator { get; set; }
        public ICommand UpdateCurrentViewModelCommand { get; }
        public string Language { get; set; }

        #endregion

        #region Commands

        public DelegateCommand MinimizeWindowCommand { get; private set; }
        public DelegateCommand CloseWindowCommand { get; private set; }
        public DelegateCommand LogoutCommand { get; set; }

        #endregion

        #region Construtor

        public MainViewModel(INavigator navigator,
            ISwiftResumeViewModelFactory viewModelFactory,
            IAuthenticator authenticator,
            ViewModelDelegateRenavigator<LoginViewModel> viewModelDelegateRenavigator,
            IDialogService dialogService,
            YesNoDialogViewModel yesNoDialogViewModel)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
            _authenticator = authenticator;
            _viewModelDelegateRenavigator = viewModelDelegateRenavigator;
            _dialogService = dialogService;
            _yesNoDialogViewModel = yesNoDialogViewModel;

            _navigator.StateChanged += Navigator_StateChanged;
            _authenticator.StateChanged += Authenticator_StateChanged;
            
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory);
            //Startup View
            UpdateCurrentViewModelCommand.Execute(EnumLanguage.ViewType.Login);

            MinimizeWindowCommand = new DelegateCommand(OnMinimizeCommand);
            CloseWindowCommand = new DelegateCommand(OnCloseCommand);
            LogoutCommand = new DelegateCommand(OnLogout);
        }

        private void OnLogout()
        {
            _yesNoDialogViewModel.Message = "¿Está seguro que desea cerrar sesión?";
            var result = _dialogService.OpenDialog(_yesNoDialogViewModel);

            if (result == DialogResults.Si)
            {
                _authenticator.Logout();
                (_viewModelDelegateRenavigator as IRenavigator).Renavigate();
            }
        }

        private void Authenticator_StateChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
            OnPropertyChanged(nameof(Username));
        }

        private void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
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
            _authenticator.StateChanged -= Authenticator_StateChanged;

            base.Dispose();
        }

        #endregion
    }
}
