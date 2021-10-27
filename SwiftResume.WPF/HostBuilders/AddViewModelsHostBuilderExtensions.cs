using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SwiftResume.WPF.Core;
using SwiftResume.WPF.CustomControls.Dialogs.Resume;
using SwiftResume.WPF.State.Authenticators;
using SwiftResume.WPF.State.Navigators;
using SwiftResume.WPF.ViewModels;
using SwiftResume.WPF.ViewModels.Factories;
using System;

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
                services.AddSingleton<CreateViewModel<LoginViewModel>>(services => () => CreateLoginViewModel(services));
                services.AddSingleton<CreateViewModel<RegisterViewModel>>(services => () => CreateRegisterViewModel(services));

                services.AddSingleton<ViewModelDelegateRenavigator<ResumeViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<RegisterViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<LoginViewModel>>();

                services.AddSingleton<ISwiftResumeViewModelFactory, SwiftResumeViewModelFactory>();
            });

            return host;
        }

        private static LoginViewModel CreateLoginViewModel(IServiceProvider services)
        {
            return new LoginViewModel(
                        services.GetRequiredService<IAuthenticator>(),
                        services.GetRequiredService<ViewModelDelegateRenavigator<ResumeViewModel>>(),
                        services.GetRequiredService<ViewModelDelegateRenavigator<RegisterViewModel>>());
        }

        private static RegisterViewModel CreateRegisterViewModel(IServiceProvider services)
        {
            return new RegisterViewModel(
                        services.GetRequiredService<IAuthenticator>(),
                        services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>(),
                        services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>());
        }

    }
}
