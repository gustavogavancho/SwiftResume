using SwiftResume.WPF.State.Authenticators;
using SwiftResume.WPF.State.Navigators;

namespace SwiftResume.WPF.HostBuilders;

public static class AddStoresHostBuilderExtensions
{
    public static IHostBuilder AddStores(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            services.AddSingleton<IAuthenticator, Authenticator>();
            services.AddSingleton<INavigator, Navigator>();
        });

        return host;
    }
}