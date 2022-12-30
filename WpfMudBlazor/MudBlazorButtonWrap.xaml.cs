using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfMudBlazor
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class MudBlazorButtonWrap : UserControl, INotifyPropertyChanged
    {
        private string text = string.Empty;

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

        public MudBlazorButtonWrap()
        {
            InitializeComponent();
            this.DataContext = this;

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
