using System;

namespace WpfMudBlazor.Models;
public interface ISerialCommunication
{
    /// <summary>
    /// Occurs when something changes on serial line.
    /// </summary>
    event EventHandler<string> SerialChanged;
    /// <summary>
    /// Occurs when serial line is connected.
    /// </summary>
    event EventHandler<string> SerialConnected;
    /// <summary>
    /// Occurs when serial line is disconnected.
    /// </summary>
    event EventHandler<string> SerialDisconnected;

    /// <summary>
    /// Connects to serial line.
    /// </summary>
    /// <param name="portName">Name of the port.</param>
    /// <param name="baudRate">The baud rate.</param>
    void ConnectToSerial(string portName, int baudRate);
    /// <summary>
    /// Disconnects from serial line.
    /// </summary>
    void Disconnect();
    /// <summary>
    /// Command send on serial line as single char.
    /// </summary>
    /// <param name="data">The data.</param>
    void SerialCmdSend(char data);
    /// <summary>
    /// Command send on serial line as multiple char (string).
    /// </summary>
    /// <param name="data">The data.</param>
    void SerialCmdSend(string data);
}
