using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows.Controls;
using WpfMudBlazor.Models;
using WpfMudBlazor.Services;

namespace WpfMudBlazor;

/// <summary>
/// Interaction logic for WpfTextAreaWrap.xaml
/// </summary>
public partial class WpfTextAreaWrap : UserControl
{
    private string text = string.Empty;
    public string Text
    {
        get
        {
            return text;
        }
        set
        {
            text = value;
            if (textAreaParameters.Parameters is null)
            {
                textAreaParameters.Parameters = new Dictionary<string, object?>();
            }
            textAreaParameters.Parameters.Add("Text", Text);
        }
    }

    public WpfTextAreaWrap()
    {
        InitializeComponent();
 
    }

}
