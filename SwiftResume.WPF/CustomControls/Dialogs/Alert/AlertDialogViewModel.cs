using Prism.Commands;
using SwiftResume.COMMON.Enums;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using System;
using System.Windows.Input;

namespace SwiftResume.WPF.CustomControls.Dialogs.Alert
{
    public class AlertDialogViewModel : DialogViewModelBase<DialogResults>
    {
        public ICommand OkCommand { get; private set; }

        public AlertDialogViewModel(string title, string message) : base(title, message)
        {
            OkCommand = new DelegateCommand<IDialogWindow>(OnOk);
        }

        private void OnOk(IDialogWindow window)
        {
            CloseDialogWithResult(window, DialogResults.Indefinido);
        }
    }
}
