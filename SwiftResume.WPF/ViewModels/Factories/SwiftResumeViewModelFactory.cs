using SwiftResume.COMMON.Enums;
using SwiftResume.WPF.Core;

namespace SwiftResume.WPF.ViewModels.Factories;

public class SwiftResumeViewModelFactory : ISwiftResumeViewModelFactory
{
    private readonly CreateViewModel<ResumeViewModel> _createResumeViewModel;
    private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
    private readonly CreateViewModel<EditViewModel> _createEditViewModel;

    public SwiftResumeViewModelFactory(CreateViewModel<ResumeViewModel> createResumeViewModel,
        CreateViewModel<LoginViewModel> createLoginViewModel,
        CreateViewModel<EditViewModel> createEditViewModel)
    {
        _createResumeViewModel = createResumeViewModel;
        _createLoginViewModel = createLoginViewModel;
        _createEditViewModel = createEditViewModel;
    }

    public ViewModelBase CreateViewModel(ViewType viewType) => viewType switch
    {
        ViewType.Resume => _createResumeViewModel(),
        ViewType.Login => _createLoginViewModel(),
        ViewType.Edit => _createEditViewModel(),
        _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType")
    };
}
