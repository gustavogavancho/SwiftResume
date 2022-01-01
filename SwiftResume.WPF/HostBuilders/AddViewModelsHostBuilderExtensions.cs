using SwiftResume.WPF.Core;
using SwiftResume.WPF.CustomControls.Dialogs.Alert;
using SwiftResume.WPF.CustomControls.Dialogs.Certificacion;
using SwiftResume.WPF.CustomControls.Dialogs.Educacion;
using SwiftResume.WPF.CustomControls.Dialogs.Experiencia;
using SwiftResume.WPF.CustomControls.Dialogs.Habilidad;
using SwiftResume.WPF.CustomControls.Dialogs.Report;
using SwiftResume.WPF.CustomControls.Dialogs.Resume;
using SwiftResume.WPF.CustomControls.Dialogs.YesNo;
using SwiftResume.WPF.State.Navigators;
using SwiftResume.WPF.ViewModels;
using SwiftResume.WPF.ViewModels.Factories;

namespace SwiftResume.WPF.HostBuilders;

public static class AddViewModelsHostBuilderExtensions
{
    public static IHostBuilder AddViewModels(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<ResumeViewModel>();
            services.AddSingleton<EditViewModel>();
            services.AddSingleton<RegisterViewModel>();
            services.AddSingleton<LoginViewModel>();

            services.AddSingleton<PerfilViewModel>();
            services.AddSingleton<IdiomasHabilidadesSoftwareViewModel>();
            services.AddSingleton<EducacionExperienciaViewModel>();
            services.AddSingleton<CertificacionProyectoViewModel>();
            services.AddSingleton<InfoAdicionalViewModel>();

            services.AddSingleton<ResumeDialogViewModel>();
            services.AddSingleton<ReportDialogViewModel>();
            services.AddSingleton<HabilidadDialogViewModel>();
            services.AddSingleton<EducacionDialogViewModel>();
            services.AddSingleton<ExperienciaDialogViewModel>();
            services.AddSingleton<CertificacionDialogViewModel>();
            services.AddSingleton<AlertDialogViewModel>();
            services.AddSingleton<YesNoDialogViewModel>();

            services.AddSingleton<CreateViewModel<ResumeViewModel>>(services => () => services.GetRequiredService<ResumeViewModel>());
            services.AddSingleton<CreateViewModel<LoginViewModel>>(services => () => services.GetRequiredService<LoginViewModel>());
            services.AddSingleton<CreateViewModel<RegisterViewModel>>(services => () => services.GetRequiredService<RegisterViewModel>());
            services.AddSingleton<CreateViewModel<EditViewModel>>(services => () => services.GetRequiredService<EditViewModel>());
            services.AddSingleton<CreateViewModel<PerfilViewModel>>(services => () => services.GetRequiredService<PerfilViewModel>());
            services.AddSingleton<CreateViewModel<IdiomasHabilidadesSoftwareViewModel>>(services => () => services.GetRequiredService<IdiomasHabilidadesSoftwareViewModel>());
            services.AddSingleton<CreateViewModel<EducacionExperienciaViewModel>>(services => () => services.GetRequiredService<EducacionExperienciaViewModel>());
            services.AddSingleton<CreateViewModel<CertificacionProyectoViewModel>>(services => () => services.GetRequiredService<CertificacionProyectoViewModel>());
            services.AddSingleton<CreateViewModel<InfoAdicionalViewModel>>(services => () => services.GetRequiredService<InfoAdicionalViewModel>());

            services.AddSingleton<ViewModelDelegateRenavigator<ResumeViewModel>>();
            services.AddSingleton<ViewModelDelegateRenavigator<RegisterViewModel>>();
            services.AddSingleton<ViewModelDelegateRenavigator<LoginViewModel>>();
            services.AddSingleton<ViewModelDelegateRenavigator<EditViewModel>>();

            services.AddSingleton<ISwiftResumeViewModelFactory, SwiftResumeViewModelFactory>();
        });

        return host;
    }
}