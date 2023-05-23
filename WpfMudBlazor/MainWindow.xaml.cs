﻿using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
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

        private string text = string.Empty;

        private string buttonText = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ISerialCommunication SerialCommunication { get; set; }
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

        /// <summary>
        /// Gets or sets the name of the COM.
        /// </summary>
        /// <value>
        /// The name of the COM.
        /// </value>
        public string ComName { get; set; }

        /// <summary>
        /// Gets or sets the baud rate.
        /// </summary>
        /// <value>
        /// The baud rate.
        /// </value>
        public string BaudRate { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;
            comboBoxCom.SelectedIndex = 0;
            comboBoxBaud.SelectedIndex = 0;

            SerialCommunication =App.AppHost.Services.GetRequiredService<ISerialCommunication>();
            SerialCommunication.SerialChanged += SerialCommunication_SerialChanged;
            SerialCommunication.SerialConnected += SerialCommunication_SerialConnected;
            SerialCommunication.SerialDisconnected += SerialCommunication_SerialDisconnected;

            eventAggregator = App.AppHost.Services.GetRequiredService<IEventAggregator>();
            eventAggregatorService = App.AppHost.Services.GetRequiredService<EventAggregatorService>();
            eventAggregatorService.OnButtonPressed += EventAggregatorService_OnButtonPressed; ;
            eventAggregatorService.OnTextChanged += EventAggregatorService_OnTextChanged;
        }

        private void SerialCommunication_SerialDisconnected(object? sender, string e)
        {
            throw new System.NotImplementedException();
        }

        private void SerialCommunication_SerialConnected(object? sender, string e)
        {
            throw new System.NotImplementedException();
        }

        private void SerialCommunication_SerialChanged(object? sender, string e)
        {
            throw new System.NotImplementedException();
        }

        private void EventAggregatorService_OnTextChanged(object? sender, string e)
        {
            
        }

        private void EventAggregatorService_OnButtonPressed(object? sender, string e)
        {
            Text = "";
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
            BaudRate = selectedItem.Content.ToString();
        }
    }
}
