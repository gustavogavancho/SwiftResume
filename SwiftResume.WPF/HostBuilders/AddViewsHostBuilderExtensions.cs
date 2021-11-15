using SwiftResume.WPF.CustomControls.Dialogs.Alert;
using SwiftResume.WPF.ViewModels;
using SwiftResume.WPF.Views;

namespace SwiftResume.WPF.HostBuilders;

public static class AddViewsHostBuilderExtensions
{
    public static IHostBuilder AddViews(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            services.AddSingleton<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
            services.AddSingleton<AlertDialogView>();
        });

        return host;
    }
}