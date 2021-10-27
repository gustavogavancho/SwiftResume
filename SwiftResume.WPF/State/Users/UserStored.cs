using SwiftResume.COMMON.Models;
using System;

namespace SwiftResume.WPF.State.Users
{
    public class UserStored : IUserStored
    {
        private User _currentUser;

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                StateChanged?.Invoke();
            }
        }

        public event Action StateChanged;
    }
}
