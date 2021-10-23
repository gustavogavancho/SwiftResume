using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SwiftResume.DAL.EFCORE;
using SwiftResume.WPF.HostBuilders;
using SwiftResume.WPF.Views;
using System.Globalization;
using System.Windows;
using WPFLocalizeExtension.Engine;

namespace SwiftResume.WPF
{
    public partial class App : Application
    {
        private readonly IHost _host;
        public App()
        {
            _host = CreateHostBuilder().Build();

            LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
            LocalizeDictionary.Instance.Culture = new CultureInfo("en-US");
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .AddConfiguration()
                //.AddFinanceApi()
                .AddDbContext()
                .AddServices()
                .AddStores()
                .AddViewModels()
                .AddViews();
        }

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            await _host.StartAsync();

            SwiftResumeDbContextFactory contextFactory = _host.Services.GetRequiredService<SwiftResumeDbContextFactory>();
            using (SwiftResumeDbContext context = contextFactory.CreateDbContext())
            {
                context.Database.Migrate();
            }

            Window window = _host.Services.GetRequiredService<MainWindow>();
            window.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StartAsync();
            _host.Dispose();
            base.OnExit(e);
        }
    }
}
