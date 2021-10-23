using Prism.Commands;
using SwiftResume.COMMON.Enums;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using System.Windows.Input;

namespace SwiftResume.WPF.CustomControls.Dialogs.YesNo
{
    public class YesNoDialogViewModel : DialogViewModelBase<DialogResults>
    {
        public ICommand YesCommand { get; private set; }
        public ICommand NoCommand { get; private set; }

        public YesNoDialogViewModel(string message) : base(message)
        {
            YesCommand = new DelegateCommand<IDialogWindow>(OnYes);
            NoCommand = new DelegateCommand<IDialogWindow>(No);
        }

        private void OnYes(IDialogWindow window)
        {
            CloseDialogWithResult(window, DialogResults.Si);
        }

        private void No(IDialogWindow window)
        {
            CloseDialogWithResult(window, DialogResults.No);
        }
    }
}
