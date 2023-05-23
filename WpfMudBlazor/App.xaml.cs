using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using WpfMudBlazor.Models;
using WpfMudBlazor.Services;

namespace WpfMudBlazor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }

        public App()
        {
            AppHost = Host
                .CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<MainWindow>();

                    services.AddWpfBlazorWebView();
                    services.AddMudServices();

                    services.AddSingleton<IEventAggregator, EventAggregator>();
                    services.AddSingleton<EventAggregatorService>();

                    services.AddSingleton<ISerialCommunication, SerialCommunication>();


                    Resources.Add("services", services.BuildServiceProvider());

                    IConfiguration configuration;
                    configuration = new ConfigurationBuilder()
                        .AddJsonFile(@"appsettings.json")
                        .Build();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
            startupForm.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();

            base.OnExit(e);
        }
    }
}
