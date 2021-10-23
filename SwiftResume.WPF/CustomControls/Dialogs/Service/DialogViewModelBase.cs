namespace SwiftResume.WPF.CustomControls.Dialogs.Service
{
    public abstract class DialogViewModelBase<T>
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public T DialogResult { get; set; }

        protected DialogViewModelBase() : this(string.Empty, string.Empty) { }
        protected DialogViewModelBase(string title) : this(title, string.Empty) { }
        protected DialogViewModelBase(string title, string message)
        {
            Title = title;
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
