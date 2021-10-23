using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.CustomControls.Dialogs.Alert;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
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

                services.AddSingleton<AlertDialogView>();

                services.AddSingleton<CreateViewModel<ResumeViewModel>>(services => () => services.GetRequiredService<ResumeViewModel>());

                services.AddSingleton<ISwiftResumeViewModelFactory, SwiftResumeViewModelFactory>();

                services.AddSingleton<IDialogService, DialogService>();

            });

            return host;
        }
    }
}
