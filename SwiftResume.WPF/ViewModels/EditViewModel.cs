using SwiftResume.WPF.Core;
using SwiftResume.WPF.State.Navigators;

namespace SwiftResume.WPF.ViewModels;

public class EditViewModel : ViewModelBase
{
    #region Fields

    private readonly ViewModelDelegateRenavigator<ResumeViewModel> _resumeRenavigator;

    #endregion

    #region Commands
    public DelegateCommand ReturnCommand { get; private set; }
    #endregion

    #region Constructor

    public EditViewModel(ViewModelDelegateRenavigator<ResumeViewModel> resumeRenavigator)
    {
        _resumeRenavigator = resumeRenavigator;
        ReturnCommand = new DelegateCommand(OnReturn);
    }

    private void OnReturn()
    {
        _resumeRenavigator.Renavigate();
    }

    #endregion

    #region Methods

    #endregion
}
