using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SwiftResume.BIZ.Repositories;
using SwiftResume.WPF.CustomControls.Dialogs.Service;

namespace SwiftResume.WPF.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IDialogService, DialogService>();
                services.AddSingleton<IResumeRepository, ResumeRepository>();
            });

            return host;
        }
    }
}
