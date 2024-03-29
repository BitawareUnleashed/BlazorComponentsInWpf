﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MudBlazor;

namespace WpfMudBlazor
{
    /// <summary>
    /// Interaction logic for WpfTextfieldWtap.xaml
    /// </summary>
    public partial class WpfTextfieldWrap : UserControl, INotifyPropertyChanged
    {
        private string label;
        

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Label
        {
            get => label;
            set
            {
                label = value;
                if (textFieldParameters.Parameters is null)
                {
                    textFieldParameters.Parameters = new Dictionary<string, object?>();
                }
                textFieldParameters.Parameters.Add("LabelText", Label); 

                OnPropertyChanged(nameof(Label));
            }
        }

        private InputType inputType = InputType.Text;
        public InputType InputType
        {
            get => inputType;
            set
            {
                inputType = value;
                if (textFieldParameters.Parameters is null)
                {
                    textFieldParameters.Parameters = new Dictionary<string, object?>();
                }
                textFieldParameters.Parameters.Add("InputType", InputType);

                OnPropertyChanged(nameof(InputType));
            }
        }

        public WpfTextfieldWrap()
        {
            InitializeComponent();
        }

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
