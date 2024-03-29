﻿using SwiftResume.COMMON.Enums;
using SwiftResume.WPF.Core;

namespace SwiftResume.WPF.ViewModels.Factories;

public class SwiftResumeViewModelFactory : ISwiftResumeViewModelFactory
{
    private readonly CreateViewModel<ResumeViewModel> _createResumeViewModel;
    private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
    private readonly CreateViewModel<EditViewModel> _createEditViewModel;
    private readonly CreateViewModel<PerfilViewModel> _createPerfilViewModel;
    private readonly CreateViewModel<IdiomasHabilidadesSoftwareViewModel> _createIdiomasHabilidadesSoftwareViewModel;
    private readonly CreateViewModel<EducacionExperienciaViewModel> _createEducacionExperienciaViewModel;
    private readonly CreateViewModel<CertificacionProyectoViewModel> _createCertificacionViewModel;
    private readonly CreateViewModel<InfoAdicionalViewModel> _createInfoAdicionalViewModel;

    public SwiftResumeViewModelFactory(CreateViewModel<ResumeViewModel> createResumeViewModel,
        CreateViewModel<LoginViewModel> createLoginViewModel,
        CreateViewModel<EditViewModel> createEditViewModel,
        CreateViewModel<PerfilViewModel> createPerfilViewModel,
        CreateViewModel<IdiomasHabilidadesSoftwareViewModel> createIdiomasHabilidadesSoftwareViewModel,
        CreateViewModel<EducacionExperienciaViewModel> createEducacionExperienciaViewModel,
        CreateViewModel<CertificacionProyectoViewModel> createCertificacionViewModel,
        CreateViewModel<InfoAdicionalViewModel> createInfoAdicionalViewModel)
    {
        _createResumeViewModel = createResumeViewModel;
        _createLoginViewModel = createLoginViewModel;
        _createEditViewModel = createEditViewModel;
        _createPerfilViewModel = createPerfilViewModel;
        _createIdiomasHabilidadesSoftwareViewModel = createIdiomasHabilidadesSoftwareViewModel;
        _createEducacionExperienciaViewModel = createEducacionExperienciaViewModel;
        _createCertificacionViewModel = createCertificacionViewModel;
        _createInfoAdicionalViewModel = createInfoAdicionalViewModel;
    }

    public ViewModelBase CreateViewModel(ViewType viewType) => viewType switch
    {
        ViewType.Resume => _createResumeViewModel(),
        ViewType.Login => _createLoginViewModel(),
        ViewType.Edit => _createEditViewModel(),
        ViewType.Perfil => _createPerfilViewModel(),
        ViewType.IdiomasHabilidadesSoftware => _createIdiomasHabilidadesSoftwareViewModel(),
        ViewType.EducacionExperiencia => _createEducacionExperienciaViewModel(),
        ViewType.CertificacionProyecto => _createCertificacionViewModel(),
        ViewType.InfoAdicional => _createInfoAdicionalViewModel(),
        _ => throw new ArgumentException("The ViewType does not have a ViewModel.")
    };
}
