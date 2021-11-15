using SwiftResume.COMMON.Enums;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.State.Authenticators;
using SwiftResume.WPF.State.Navigators;

namespace SwiftResume.WPF.ViewModels;

public class RegisterViewModel : ViewModelBase
{
    #region Fields

    private readonly IAuthenticator _authenticator;
    private readonly IRenavigator _loginRenavigator;

    #endregion

    #region Properties

    private string _email;
    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }

    private string _username;
    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
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
        }
    }

    private string _confirmPassword;
    public string ConfirmPassword
    {
        get => _confirmPassword;
        set
        {
            _confirmPassword = value;
            OnPropertyChanged(nameof(ConfirmPassword));
            OnPropertyChanged(nameof(CanRegister));
        }
    }

    public bool CanRegister => !string.IsNullOrWhiteSpace(Email) &&
        !string.IsNullOrWhiteSpace(Username) &&
        !string.IsNullOrWhiteSpace(Password) &&
        !string.IsNullOrWhiteSpace(ConfirmPassword);

    #endregion

    #region Commands

    public DelegateCommand RegisterCommand { get; set; }
    public DelegateCommand ViewLoginCommand { get; set; }

    #endregion

    #region Constructor

    public RegisterViewModel(IAuthenticator authenticator,
        IRenavigator loginRenavigator)
    {
        _authenticator = authenticator;
        _loginRenavigator = loginRenavigator;

        RegisterCommand = new DelegateCommand(OnRegister, CanRegisterUser);
        ViewLoginCommand = new DelegateCommand(OnViewLogin);

        this.PropertyChanged += RegisterViewModel_PropertyChanged;
    }

    #endregion

    #region Methods

    private void OnViewLogin()
    {
        _loginRenavigator.Renavigate();
    }

    private void RegisterViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(CanRegister))
        {
            RegisterCommand.RaiseCanExecuteChanged();
        }
    }

    private async void OnRegister()
    {
        try
        {
            RegistrationResult registrationResult = await _authenticator.Register(
                Email,
                Username,
                Password,
                ConfirmPassword);

            switch (registrationResult)
            {
                case RegistrationResult.Success:
                    _loginRenavigator.Renavigate();
                    break;
                case RegistrationResult.PasswordsNoDotMatch:
                    //TODO: Implement password do not match
                    break;
                case RegistrationResult.EmailAlreadyExists:
                    //TODO: Implement email already exists
                    break;
                case RegistrationResult.UsernameAlreadyExists:
                    //TODO: Implement username already exists
                    break;
                default:
                    //TODO: Implement registration already exists
                    break;
            }
        }
        catch (Exception ex)
        {
            //TODO: Implement catch exception
        }
    }

    private bool CanRegisterUser()
    {
        return CanRegister;
    }

    #endregion
}