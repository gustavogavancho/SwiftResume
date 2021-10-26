using Prism.Commands;
using SwiftResume.WPF.Core;

namespace SwiftResume.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
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

        public DelegateCommand LoginCommand { get; }

        #endregion

        #region Constructors
        public LoginViewModel()
        {

        }
        #endregion
    }
}
