using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using WpfMudBlazor.Models;
using WpfMudBlazor.Services;

namespace WpfMudBlazor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private IEventAggregator? eventAggregator;
        private EventAggregatorService? eventAggregatorService;


        private string buttonText = "MY BUTTON";

        public event PropertyChangedEventHandler? PropertyChanged;

        public string ButtonText
        {
            get => buttonText;
            set
            {
                buttonText = value.ToUpper();
                OnPropertyChanged(nameof(ButtonText));
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            eventAggregator = App.AppHost.Services.GetRequiredService<IEventAggregator>();

            //var serviceCollection = new ServiceCollection();
            //serviceCollection.AddWpfBlazorWebView();
            //serviceCollection.AddMudServices();
            //serviceCollection.AddSingleton<IEventAggregator, EventAggregator>();
            //Resources.Add("services", serviceCollection.BuildServiceProvider());

            eventAggregator = App.AppHost.Services.GetRequiredService<IEventAggregator>();
            eventAggregatorService = App.AppHost.Services.GetRequiredService<EventAggregatorService>();
            eventAggregatorService.OnButtonPressed += EventAggregatorService_OnButtonPressed; ;
        }

        private void EventAggregatorService_OnButtonPressed(object? sender, string e)
        {
            this.ButtonText = e;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            eventAggregator.PublishEvent(new ButtonPressed() { ButtonName = "WPF" });
        }

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
