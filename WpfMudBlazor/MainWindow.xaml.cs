using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using WpfMudBlazor.Models;
using WpfMudBlazor.Services;

namespace WpfMudBlazor;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{
    private IEventAggregator? eventAggregator;
    private EventAggregatorService? eventAggregatorService;

    private string command = string.Empty;

    private string text = string.Empty;

    private string buttonText = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    public ISerialCommunication SerialCommunication { get; set; }

    private bool isConnected = true;
    public bool IsConnected
    {
        get
        {
            return isConnected;
        }
        set
        {
            isConnected = value;
            OnPropertyChanged(nameof(IsConnected));
        }
    }

    private bool isNotConnected = false;
    public bool IsNotConnected
    {
        get
        {
            return isNotConnected;
        }
        set
        {
            isNotConnected = value;
            OnPropertyChanged(nameof(IsNotConnected));
        }
    }

    /// <summary>
    /// Gets or sets the text.
    /// </summary>
    /// <value>
    /// The text.
    /// </value>
    public string Text
    {
        get => text;
        set
        {
            text = value;
            OnPropertyChanged(nameof(Text));
        }
    }

    /// <summary>
    /// Gets or sets the name of the COM.
    /// </summary>
    /// <value>
    /// The name of the COM.
    /// </value>
    public string ComName { get; set; } = "COM1";

    /// <summary>
    /// Gets or sets the baud rate.
    /// </summary>
    /// <value>
    /// The baud rate.
    /// </value>
    public int BaudRate { get; set; } = 9600;

    public MainWindow()
    {
        InitializeComponent();

        this.DataContext = this;
        comboBoxCom.SelectedIndex = 0;
        comboBoxBaud.SelectedIndex = 0;

        SerialCommunication = App.AppHost.Services.GetRequiredService<ISerialCommunication>();

        eventAggregator = App.AppHost.Services.GetRequiredService<IEventAggregator>();
        eventAggregatorService = App.AppHost.Services.GetRequiredService<EventAggregatorService>();
        eventAggregatorService.OnButtonPressed += EventAggregatorService_OnButtonPressed; ;
        eventAggregatorService.OnTextChanged += EventAggregatorService_OnTextChanged;


        SerialCommunication = App.AppHost.Services.GetRequiredService<ISerialCommunication>();
        SerialCommunication.SerialConnected += SerialCommunication_SerialConnected;
        SerialCommunication.SerialDisconnected += SerialCommunication_SerialDisconnected;

    }

    private void SerialCommunication_SerialDisconnected(object? sender, string e)
    {
        IsConnected = true;
        IsNotConnected = false;
    }

    private void SerialCommunication_SerialConnected(object? sender, string e)
    {
        IsConnected = false;
        IsNotConnected = true;
    }

    private void EventAggregatorService_OnTextChanged(object? sender, string e)
    {
        command = e;
    }

    private void EventAggregatorService_OnButtonPressed(object? sender, string e)
    {
        switch (e)
        {
            case "disconnect-button":
                SerialCommunication.Disconnect();
                break;
            case "connect-button":
                SerialCommunication.ConnectToSerial(ComName, BaudRate);
                break;
            case "send-button":
                SerialCommunication.SerialCmdSend(command);
                break;
            case "1-button":
                SerialCommunication.SerialCmdSend((char)0x01);
                break;
            case "2-button":
                SerialCommunication.SerialCmdSend((char)0x02);
                break;
            case "3-button":
                SerialCommunication.SerialCmdSend((char)0x03);
                break;
            case "4-button":
                SerialCommunication.SerialCmdSend((char)0x04);
                break;
            case "5-button":
                SerialCommunication.SerialCmdSend((char)0x05);
                break;
            case "6-button":
                SerialCommunication.SerialCmdSend((char)0x06);
                break;
            case "7-button":
                SerialCommunication.SerialCmdSend((char)0x07);
                break;
            case "8-button":
                SerialCommunication.SerialCmdSend((char)0x08);
                break;
            case "9-button":
                SerialCommunication.SerialCmdSend((char)0x09);
                break;
            case "A-button":
                SerialCommunication.SerialCmdSend((char)0x0A);
                break;
            case "B-button":
                SerialCommunication.SerialCmdSend((char)0x0B);
                break;
            case "C-button":
                SerialCommunication.SerialCmdSend((char)0x0C);
                break;
            case "D-button":
                SerialCommunication.SerialCmdSend((char)0x0D);
                break;
            case "E-button":
                SerialCommunication.SerialCmdSend((char)0x0E);
                break;
            case "F-button":
                SerialCommunication.SerialCmdSend((char)0x0F);
                break;
            case "10-button":
                SerialCommunication.SerialCmdSend((char)0x10);
                break;
            case "11-button":
                var t = ((char)0x02).ToString() + ((char)2).ToString() + ((char)4).ToString() + ((char)13).ToString() + ((char)1).ToString() + ';' + ((char)64).ToString() + '|';
                SerialCommunication.SerialCmdSend(t);
                break;
            case "12-button":
                var t1 = ((char)2).ToString() + ((char)2).ToString() + ((char)4).ToString() + ((char)13).ToString() + ((char)1).ToString() + ';' + ((char)2).ToString() + '|';
                SerialCommunication.SerialCmdSend(t1);
                break;
            default:
                break;
        }
    }

    private void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void comboBoxCom_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        ComboBox comboBox = (ComboBox)sender;
        ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;

        // Esempio di utilizzo del valore selezionato
        ComName = selectedItem.Content.ToString();
    }

    private void comboBoxBaud_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        ComboBox comboBox = (ComboBox)sender;
        ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;

        // Esempio di utilizzo del valore selezionato
        int.TryParse(selectedItem.Content.ToString(), out int baudRate);
        BaudRate = baudRate;
    }
}
