using SwiftResume.WPF.Core;

namespace SwiftResume.WPF.CustomControls.Dialogs.Service
{
    public abstract class DialogViewModelBase<T> : ViewModelBase
    {
        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public T DialogResult { get; set; }

        protected void CloseDialogWithResult(IDialogWindow dialog, T result)
        {
            DialogResult = result;

            if (dialog != null)
                dialog.DialogResult = true;
        }
    }
}
