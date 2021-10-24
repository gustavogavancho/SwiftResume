using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.CustomControls.Dialogs.Resume;
using SwiftResume.WPF.ViewModels;
using SwiftResume.WPF.ViewModels.Factories;

namespace SwiftResume.WPF.HostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<ResumeViewModel>();

                services.AddTransient<ResumeDialogViewModel>();

                services.AddSingleton<CreateViewModel<ResumeViewModel>>(services => () => services.GetRequiredService<ResumeViewModel>());

                services.AddSingleton<ISwiftResumeViewModelFactory, SwiftResumeViewModelFactory>();
            });

            return host;
        }
    }
}
