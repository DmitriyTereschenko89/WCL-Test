﻿using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Wpf_Wcl.Common;
using Wpf_Wcl.Services;
using Wpf_Wcl.CustomControls;
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
            _ = services.AddScoped<MainWindow>();
            _ = services.AddScoped<UserLoginControl>();
            _ = services.AddScoped<BindablePasswordBox>();
            _ = services.AddScoped<LoginViewModel>();
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
