using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wpf_Wcl.Common;
using Wpf_Wcl.Configurations;
using Wpf_Wcl.CustomControls;
using Wpf_Wcl.Profiles;
using Wpf_Wcl.Services;
using Wpf_Wcl.ViewModels;

namespace Wpf_Wcl
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        private void ConfigureServices(ServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            _ = services.AddHttpClient("apiservice", (serviceProvider, client) =>
            {
                client.DefaultRequestHeaders.Add("accept", "application/json");
                client.BaseAddress = new Uri("https://petstore.swagger.io/v2/");
            });

            _ = services.Configure<DefaultConfig>(configuration.GetSection("DefaultConfig"));
            _ = services.AddAutoMapper(typeof(UserProfile));
            _ = services.AddScoped<MainWindow>();
            _ = services.AddScoped<UserLoginControl>();
            _ = services.AddScoped<BindablePasswordBox>();
            _ = services.AddSingleton<LoginViewModel>();
            _ = services.AddScoped<RegistrationViewModel>();
            _ = services.AddSingleton<MainWindowViewModel>();
            _ = services.AddScoped<IApiService, ApiService>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }

        public App()
        {
            ServiceCollection serviceCollections = new ServiceCollection();
            ConfigureServices(serviceCollections);
            _serviceProvider = serviceCollections.BuildServiceProvider();
        }
    }
}
