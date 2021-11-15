using SwiftResume.COMMON.Enums;
using SwiftResume.WPF.CustomControls.Dialogs.Service;

namespace SwiftResume.WPF.CustomControls.Dialogs.Alert;

public class AlertDialogViewModel : DialogViewModelBase<DialogResults>
{
    public DelegateCommand<IDialogWindow> OkCommand { get; private set; }

    public AlertDialogViewModel()
    {
        OkCommand = new DelegateCommand<IDialogWindow>(OnOk);
    }

    private void OnOk(IDialogWindow window)
    {
        CloseDialogWithResult(window, DialogResults.Indefinido);
    }
}
