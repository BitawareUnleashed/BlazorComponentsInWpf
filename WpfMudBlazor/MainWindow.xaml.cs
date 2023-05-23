using System.ComponentModel;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using WpfMudBlazor.Models;
using WpfMudBlazor.Services;


namespace WpfMudBlazor;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{
    
    public event PropertyChangedEventHandler? PropertyChanged;
   
    //public string Text
    //{
    //    get => text;
    //    set
    //    {
    //        text = value;
    //        OnPropertyChanged(nameof(Text));
    //    }
    //}

    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = this;
        SelectFirstComboBoxItem();
    }

            eventAggregator = App.AppHost.Services.GetRequiredService<IEventAggregator>();
            eventAggregatorService = App.AppHost.Services.GetRequiredService<EventAggregatorService>();
            eventAggregatorService.OnButtonPressed += EventAggregatorService_OnButtonPressed; ;
            eventAggregatorService.OnTextChanged += EventAggregatorService_OnTextChanged;
            eventAggregatorService.OnPasswordChanged += EventAggregatorService_OnPasswordChanged;
        }

    private void SelectFirstComboBoxItem()
    {
        if (serialNameComboBox.Items.Count > 0)
        {
            serialNameComboBox.SelectedIndex = 0;
        }
        if(serialBoudComboBox.Items.Count > 0)
        {
            serialBoudComboBox.SelectedIndex = 0;
        }
    }
    private void comboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        // Ottieni l'elemento selezionato
        ComboBox comboBox = (ComboBox)sender;
        string selectedValue = comboBox.SelectedItem.ToString();
    }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            
            eventAggregator?.Publish(new ButtonLogout("User logged out from WPF"));
        }



        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
