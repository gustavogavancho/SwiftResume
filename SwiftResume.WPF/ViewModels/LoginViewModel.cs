using SwiftResume.BIZ.Exceptions;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.CustomControls.Dialogs.Alert;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.State.Authenticators;
using SwiftResume.WPF.State.Navigators;

namespace SwiftResume.WPF.ViewModels;

public class LoginViewModel : ViewModelBase
{
    #region Fields

    private readonly IAuthenticator _authenticator;
    private readonly IDialogService _dialogService;
    private readonly AlertDialogViewModel _alertDialogViewModel;
    private readonly ViewModelDelegateRenavigator<ResumeViewModel> _loginRenavigator;
    private readonly ViewModelDelegateRenavigator<RegisterViewModel> _registerRenavigator;

    #endregion

    #region Properties

    public bool CanLogin => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);


    private string _username;
    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
            OnPropertyChanged(nameof(CanLogin));
        }
    }

    private string _password;
    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
            OnPropertyChanged(nameof(CanLogin));
        }
    }

    #endregion

    #region Commands

    public DelegateCommand LoginCommand { get; private set; }
    public DelegateCommand ViewRegisterCommand { get; private set; }

    #endregion

    #region Constructors
    public LoginViewModel(IAuthenticator authenticator,
        IDialogService dialogService,
        AlertDialogViewModel alertDialogViewModel,
        ViewModelDelegateRenavigator<ResumeViewModel> loginRenavigator,
        ViewModelDelegateRenavigator<RegisterViewModel> registerRenavigator)
    {
        _authenticator = authenticator;
        _dialogService = dialogService;
        _alertDialogViewModel = alertDialogViewModel;
        _loginRenavigator = loginRenavigator;
        _registerRenavigator = registerRenavigator;

        LoginCommand = new DelegateCommand(OnLogin, CanLoginin);
        ViewRegisterCommand = new DelegateCommand(OnViewRegister);

        this.PropertyChanged += LoginViewModel_PropertyChanged;
    }

    private void OnViewRegister()
    {
        _registerRenavigator.Renavigate();
    }

    private void LoginViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(CanLogin))
        {
            LoginCommand.RaiseCanExecuteChanged();
        }
    }

    private async void OnLogin()
    {
        try
        {
            await _authenticator.Login(Username, Password);

            _loginRenavigator.Renavigate();

            //Work around
            Username = "";
            Password = "";

        }
        catch (UserNotFoundException ex)
        {
            SetErrorDialog(ex);
        }
        catch (InvalidPasswordException ex)
        {
            SetErrorDialog(ex);
        }
    }

    private bool CanLoginin()
    {
        return CanLogin;
    }

    private void SetErrorDialog(Exception ex)
    {
        _alertDialogViewModel.Message = ex.Message;
        _dialogService.OpenDialog(_alertDialogViewModel);
    }

    #endregion
}
