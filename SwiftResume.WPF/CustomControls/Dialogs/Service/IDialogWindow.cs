namespace SwiftResume.WPF.CustomControls.Dialogs.Service;

public interface IDialogWindow
{
    bool? DialogResult { get; set; }
    object DataContext { get; set; }

    bool? ShowDialog();
}