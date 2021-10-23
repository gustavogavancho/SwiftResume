namespace SwiftResume.WPF.CustomControls.Dialogs.Service
{
    public abstract class DialogViewModelBase<T>
    {
        public string Message { get; set; }
        public T DialogResult { get; set; }

        protected DialogViewModelBase() : this(string.Empty) { }
        protected DialogViewModelBase(string message)
        {
            Message = message;
        }

        protected void CloseDialogWithResult(IDialogWindow dialog, T result)
        {
            DialogResult = result;

            if (dialog != null)
                dialog.DialogResult = true;
        }
    }
}
