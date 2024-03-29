﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfMudBlazor
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class WpfButtonWrap : UserControl, INotifyPropertyChanged
    {
        private string text = string.Empty;
        private string id = string.Empty;

        private string htmlStyle = string.Empty;
        public string HtmlStyle
        {
            get
            {
                return htmlStyle;
            }
            set
            {
                htmlStyle = value;
                buttonParameters.Parameters = new Dictionary<string, object>()
                {
                    { "ButtonId", Id },
                    { "ButtonText", Text },
                    { "HtmlStyle", HtmlStyle }
                }!;
                OnPropertyChanged(nameof(HtmlStyle));
            }
        }

        public string Id
        {
            get => id;
            set
            {
                id = value;
                buttonParameters.Parameters = new Dictionary<string, object>()
                {
                    { "ButtonId", Id },
                    { "ButtonText", Text }
                }!;

                OnPropertyChanged(nameof(Id));
            }
        }
        public string Text
        {
            get => text;
            set
            {
                text = value;
                buttonParameters.Parameters = new Dictionary<string, object>()
                {
                    { "ButtonText", Text }
                }!;
                OnPropertyChanged(nameof(Text));
            }
        }



        public WpfButtonWrap()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
