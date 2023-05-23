using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
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

        private string text = string.Empty;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                if (textFieldParameters.Parameters is null)
                {
                    textFieldParameters.Parameters = new Dictionary<string, object?>();
                }
                textFieldParameters.Parameters.Add("Text", Text);

                OnPropertyChanged(nameof(Text));

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
