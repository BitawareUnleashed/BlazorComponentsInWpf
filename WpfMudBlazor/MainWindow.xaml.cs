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

        private string text = "No user logged in";

        private string username = string.Empty;
        private string password = string.Empty;

        private string buttonText = "LOGIN";

        public event PropertyChangedEventHandler? PropertyChanged;


        public string Username
        {
            get => username;
            set
            {
                username = value;
                eventAggregator?.Publish(new TextChanged(Username));
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                eventAggregator?.Publish(new PasswordChanged(Password));
                OnPropertyChanged(nameof(Password));
            }
        }
        
        public string ButtonText
        {
            get => buttonText;
            set
            {
                buttonText = value.ToUpper();
                OnPropertyChanged(nameof(ButtonText));
            }
        }

        private string buttonLogoutText = "LOGOUT";
        public string ButtonLogoutText
        {
            get => buttonLogoutText;
            set
            {
                buttonLogoutText = value.ToUpper();
                OnPropertyChanged(nameof(ButtonLogoutText));
            }
        }
        
        public string Text
        {
            get => text;
            set
            {
                text = value;
                OnPropertyChanged(nameof(Text));
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
            eventAggregatorService.OnTextChanged += EventAggregatorService_OnTextChanged;
            eventAggregatorService.OnPasswordChanged += EventAggregatorService_OnPasswordChanged;
        }

        private void EventAggregatorService_OnPasswordChanged(object? sender, string e)
        {
            if (e != Password) Password = e;
        }

        private void EventAggregatorService_OnTextChanged(object? sender, string e)
        {
            if (e != Username) Username = e;
        }

        private void EventAggregatorService_OnButtonPressed(object? sender, string e)
        {
            Text = $"{e} with Username '{Username}' and password '{Password}'";
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            eventAggregator?.Publish(new ButtonLogin("User logged in from WPF"));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Username = string.Empty;
            Password=string.Empty;

            eventAggregator?.Publish(new ButtonLogout("User logged out from WPF"));
        }



        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = PasswordBox.Password;
        }
    }
}
