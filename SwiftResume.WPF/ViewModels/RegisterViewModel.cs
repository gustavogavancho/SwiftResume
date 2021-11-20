using SwiftResume.BIZ.Repositories;
using SwiftResume.COMMON.Enums;
using SwiftResume.COMMON.Models;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.CustomControls.Dialogs.Alert;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.CustomControls.Dialogs.YesNo;
using SwiftResume.WPF.State.Navigators;
using SwiftResume.WPF.Wrapper;

namespace SwiftResume.WPF.ViewModels;

public class RegisterViewModel : ViewModelBase
{
    #region Fields

    private readonly IUserRepository _userRepository;
    private readonly IRenavigator _loginRenavigator;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IDialogService _dialogService;
    private readonly AlertDialogViewModel _alertDialogViewModel;
    private readonly YesNoDialogViewModel _yesNoDialogViewModel;

    #endregion

    #region Properties

    private UserWrapper _userWrapper;
    public UserWrapper UserWrapper
    {
        get => _userWrapper;
        set 
        { 
            _userWrapper = value;
            OnPropertyChanged(nameof(UserWrapper));
        }
    }

    private bool _hasChanges;
    public bool HasChanges
    {
        get => _hasChanges;
        set 
        {
            if (_hasChanges != value) 
            {
                _hasChanges = value;
                OnPropertyChanged(nameof(HasChanges));
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }
    }


    #endregion

    #region Commands

    public DelegateCommand RegisterCommand { get; set; }
    public DelegateCommand ViewLoginCommand { get; set; }

    #endregion

    #region Constructor

    public RegisterViewModel(IUserRepository userRepository,
        IRenavigator loginRenavigator,
        IPasswordHasher<User> passwordHasher,
        IDialogService dialogService,
        AlertDialogViewModel alertDialogViewModel,
        YesNoDialogViewModel yesNoDialogViewModel)
    {
        _userRepository = userRepository;
        _loginRenavigator = loginRenavigator;
        _passwordHasher = passwordHasher;
        _dialogService = dialogService;
        _alertDialogViewModel = alertDialogViewModel;
        _yesNoDialogViewModel = yesNoDialogViewModel;
        RegisterCommand = new DelegateCommand(OnRegister, CanRegisterUser);
        ViewLoginCommand = new DelegateCommand(OnViewLogin);
    }

    #endregion

    #region Methods

    private void OnViewLogin()
    {
        if (HasChanges) 
        {
            _yesNoDialogViewModel.Message = $"Hay cambios pendientes, al cerrar la ventana se borrarán los cambios, ¿Desea cerrar la ventana?";
            var dialog = _dialogService.OpenDialog(_yesNoDialogViewModel);
            if (dialog == DialogResults.No)
                return;
        }
        _userRepository.Remove(UserWrapper.Model);
        _loginRenavigator.Renavigate();
    }

    private async void OnRegister()
    {
        UserWrapper.Validate();
        if (!UserWrapper.HasErrors)
        {
            RegistrationResult result = await _userRepository.ValidateUserRegistration(UserWrapper.Model);
            switch (result)
            {
                case RegistrationResult.Success:
                    UserWrapper.Model.PasswordHashed = _passwordHasher.HashPassword(UserWrapper.Model, UserWrapper.Model.Password);
                    await _userRepository.SaveAsync();
                    HasChanges = _userRepository.HasChanges();
                    _loginRenavigator.Renavigate();
                    break;
                case RegistrationResult.EmailAlreadyExists:
                    _alertDialogViewModel.Message = $"El correo {UserWrapper.Model.Email} ya se encuentra registrado";
                    _dialogService.OpenDialog(_alertDialogViewModel);
                    break;
                case RegistrationResult.UsernameAlreadyExists:
                    _alertDialogViewModel.Message = $"El usuario {UserWrapper.Model.Username} ya se encuentra registrado";
                    _dialogService.OpenDialog(_alertDialogViewModel);
                    break;
            }
        }
    }

    private bool CanRegisterUser()
    {
        return UserWrapper != null && !UserWrapper.HasErrors && HasChanges;
    }

    public void OnLoad() 
    {
        //Restore has changes to false
        HasChanges = false;

        var user = CreateNewUser();

        InitializeUser(user);
    }

    private void InitializeUser(User user)
    {
        UserWrapper = new UserWrapper(user);
        UserWrapper.PropertyChanged += (s, e) =>
        {
            if (!HasChanges)
                HasChanges = _userRepository.HasChanges();

            if (e.PropertyName == nameof(UserWrapper.HasErrors))
                RegisterCommand.RaiseCanExecuteChanged();
        };

        RegisterCommand.RaiseCanExecuteChanged();
    }

    private User CreateNewUser()
    {
        var user = new User();
        _userRepository.Add(user);
        return user;
    }

    #endregion
}