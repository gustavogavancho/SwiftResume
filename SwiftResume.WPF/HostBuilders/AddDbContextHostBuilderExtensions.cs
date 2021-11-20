using SwiftResume.DAL.EFCORE;

namespace SwiftResume.WPF.HostBuilders;

public static class AddDbContextHostBuilderExtensions
{
    public static IHostBuilder AddDbContext(this IHostBuilder host)
    {
        host.ConfigureServices((context, services) =>
        {
            services.AddDbContext<SwiftResumeDbContext>(options => 
            {
                options.UseSqlite(context.Configuration.GetConnectionString("sqlite"));
            }, ServiceLifetime.Singleton);
        });

        return host;
    }
}
