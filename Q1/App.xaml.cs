using Microsoft.Extensions.DependencyInjection;
using Q1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Q1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public App()
        {
            //Config for DependencyInjection (01)
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(ServiceCollection services)
        {
            //services.AddSingleton(typeof(ICarRepository), typeof(CarRepository));
            services.AddSingleton<MainWindow>();
            services.AddScoped<PRN_Spr23_B1Context>();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var windowCarMianagenent = serviceProvider.GetService<MainWindow>();
            windowCarMianagenent.Show();
        }
    }
}
