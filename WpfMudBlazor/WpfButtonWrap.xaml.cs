using Microsoft.Extensions.DependencyInjection;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
using WpfMudBlazor.Models;
using WpfMudBlazor.Services;

namespace WpfMudBlazor
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class WpfButtonWrap : UserControl, INotifyPropertyChanged
    {
        private string text = string.Empty;
        private string id = string.Empty;
        private string buttonImage = string.Empty;

        private IEventAggregator? eventAggregator;

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

        public string ButtonImage
        {
            get => buttonImage;
            set
            {
                buttonImage = value;
                buttonParameters.Parameters = new Dictionary<string, object>()
                {
                    { "ButtonId", Id },
                    { "ButtonText", Text },
                    { "ButtonImage", ButtonImage }
                }!;

                OnPropertyChanged(nameof(ButtonImage));
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
                eventAggregator?.Publish(new TextChanged(Text));
                OnPropertyChanged(nameof(Text));
            }
        }



        public WpfButtonWrap()
        {
            InitializeComponent();
            this.DataContext = this;

            eventAggregator = App.AppHost.Services.GetRequiredService<IEventAggregator>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            eventAggregator?.Publish(new ButtonClick(this.Id));
        }
    }
}
