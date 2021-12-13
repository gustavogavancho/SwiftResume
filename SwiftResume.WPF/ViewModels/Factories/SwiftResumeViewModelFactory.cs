using SwiftResume.COMMON.Enums;
using SwiftResume.WPF.Core;

namespace SwiftResume.WPF.ViewModels.Factories;

public class SwiftResumeViewModelFactory : ISwiftResumeViewModelFactory
{
    private readonly CreateViewModel<ResumeViewModel> _createResumeViewModel;
    private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
    private readonly CreateViewModel<EditViewModel> _createEditViewModel;
    private readonly CreateViewModel<PerfilViewModel> _createPerfilViewModel;
    private readonly CreateViewModel<IdiomasHabilidadesSoftwareViewModel> _createIdiomasHabilidadesSoftwareViewModel;

    public SwiftResumeViewModelFactory(CreateViewModel<ResumeViewModel> createResumeViewModel,
        CreateViewModel<LoginViewModel> createLoginViewModel,
        CreateViewModel<EditViewModel> createEditViewModel,
        CreateViewModel<PerfilViewModel> createPerfilViewModel,
        CreateViewModel<IdiomasHabilidadesSoftwareViewModel> createIdiomasHabilidadesSoftwareViewModel)
    {
        _createResumeViewModel = createResumeViewModel;
        _createLoginViewModel = createLoginViewModel;
        _createEditViewModel = createEditViewModel;
        _createPerfilViewModel = createPerfilViewModel;
        _createIdiomasHabilidadesSoftwareViewModel = createIdiomasHabilidadesSoftwareViewModel;
    }

    public ViewModelBase CreateViewModel(ViewType viewType) => viewType switch
    {
        ViewType.Resume => _createResumeViewModel(),
        ViewType.Login => _createLoginViewModel(),
        ViewType.Edit => _createEditViewModel(),
        ViewType.Perfil => _createPerfilViewModel(),
        ViewType.IdiomasHabilidadesSoftware => _createIdiomasHabilidadesSoftwareViewModel(),
        _ => throw new ArgumentException("The ViewType does not have a ViewModel.")
    };
}
