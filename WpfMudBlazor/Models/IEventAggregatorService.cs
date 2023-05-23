using System;

namespace WpfMudBlazor.Models;
public interface IEventAggregatorService
{
    /// <summary>
    /// Occurs when a button is pressed.
    /// </summary>
    event EventHandler<string> OnButtonPressed;

    /// <summary>
    /// Occurs when a text changed.
    /// </summary>
    event EventHandler<string> OnTextChanged;
}
