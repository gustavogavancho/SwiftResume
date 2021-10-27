using Prism.Commands;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.State.Authenticators;
using SwiftResume.WPF.State.Navigators;
using System;

namespace SwiftResume.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        #region Fields

        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _loginRenavigator;
        private readonly IRenavigator _registerRenavigator;

        #endregion

        #region Properties

        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
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

        public bool CanLogin => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);

        #endregion

        #region Commands

        public DelegateCommand LoginCommand { get; private set; }
        public DelegateCommand ViewRegisterCommand { get; private set; }

        #endregion

        #region Constructors
        public LoginViewModel(IAuthenticator authenticator, IRenavigator loginRenavigator, IRenavigator registerRenavigator)
        {
            _authenticator = authenticator;
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
            }
            catch (Exception ex)
            {

            }
        }

        private bool CanLoginin()
        {
            return CanLogin;
        }

        #endregion
    }
}
