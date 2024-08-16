using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Wpf_Wcl.Common;
using Wpf_Wcl.Services;
using Wpf_Wcl.UserControls;

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
            _ = services.AddSingleton<MainWindow>();
            _ = services.AddSingleton<IApiService, ApiService>();
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
