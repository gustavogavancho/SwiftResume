using SwiftResume.BIZ.Repositories;
using SwiftResume.BIZ.Services;
using SwiftResume.COMMON.Models;
using SwiftResume.WPF.CustomControls.Dialogs.Service;
using SwiftResume.WPF.State.Users;

namespace SwiftResume.WPF.HostBuilders;

public static class AddServicesHostBuilderExtensions
{
    public static IHostBuilder AddServices(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IUserStored, UserStored>();
            services.AddSingleton<IDialogService, DialogService>();

            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IResumeRepository, ResumeRepository>();
        });

        return host;
    }
}