using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;


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

    
    private void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


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

    private void comboBox_BaudRateSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        // Ottieni l'elemento selezionato
        ComboBox comboBox = (ComboBox)sender;
        string selectedValue = comboBox.SelectedItem.ToString();
    }

}
