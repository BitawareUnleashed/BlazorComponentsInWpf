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
        //private IEventAggregator? eventAggregator;
        private EventAggregatorService? eventAggregatorService;

        private string text = "No user logged in";

        private string username = string.Empty;
        private string surname = string.Empty;

        private string buttonText = "Conferma";

        public event PropertyChangedEventHandler? PropertyChanged;


        public string Name
        {
            get => username;
            set
            {
                username = value;
                eventAggregatorService?.Publish(new TextChanged(Name));
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Surname
        {
            get => surname;
            set
            {
                surname = value;
                eventAggregatorService?.Publish(new TextChanged(Surname));
                OnPropertyChanged(nameof(Surname));
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

        private string buttonLogoutText = "Annulla";
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

            //eventAggregator = App.AppHost.Services.GetRequiredService<IEventAggregator>();
            //var serviceCollection = new ServiceCollection();
            //serviceCollection.AddWpfBlazorWebView();
            //serviceCollection.AddMudServices();
            //serviceCollection.AddSingleton<IEventAggregator, EventAggregator>();
            //Resources.Add("services", serviceCollection.BuildServiceProvider());

            //eventAggregator = App.AppHost.Services.GetRequiredService<IEventAggregator>();
            eventAggregatorService = App.AppHost.Services.GetRequiredService<EventAggregatorService>();
            eventAggregatorService.OnButtonPressed += EventAggregatorService_OnButtonPressed;
            eventAggregatorService.OnTextChanged += EventAggregatorService_OnTextChanged;
            eventAggregatorService.OnPasswordChanged += EventAggregatorService_OnPasswordChanged;
            eventAggregatorService.OnButtonClickedChanged += EventAggregatorService_OnButtonClickedChanged;
        }

        private void EventAggregatorService_OnButtonClickedChanged(object? sender, string e)
        {
            Text = $"User clicked {e}'";
        }

        private void EventAggregatorService_OnPasswordChanged(object? sender, string e)
        {
            if (e != Surname) Surname = e;
        }

        private void EventAggregatorService_OnTextChanged(object? sender, string e)
        {
            if (e != Name) Name = e;
        }

        private void EventAggregatorService_OnButtonPressed(object? sender, string e)
        {
            Text = $"{e} with Name '{Name}' and surname '{Surname}'";
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            eventAggregatorService?.Publish(new ButtonConfirm("User logged in from WPF"));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Name = string.Empty;
            Surname=string.Empty;

            eventAggregatorService?.Publish(new ButtonCancel("User logged out from WPF"));
        }



        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            //Surname = PasswordBox.Surname;
        }

        bool isEdit = true;
        bool isAdd = false;
        bool isDelete = false;
        bool isSave = false;


        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            isEdit = !isEdit;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
