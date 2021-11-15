﻿using SwiftResume.DAL.EFCORE;

namespace SwiftResume.WPF.HostBuilders;

public static class AddDbContextHostBuilderExtensions
{
    public static IHostBuilder AddDbContext(this IHostBuilder host)
    {
        host.ConfigureServices((context, services) =>
        {
            string connectionString = context.Configuration.GetConnectionString("sqlite");
            Action<DbContextOptionsBuilder> configureDbContext = o => o.UseSqlite(connectionString);
            services.AddDbContext<SwiftResumeDbContext>(configureDbContext);
        });

        return host;
    }
}
