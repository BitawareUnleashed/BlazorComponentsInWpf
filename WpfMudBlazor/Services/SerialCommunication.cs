using System.Text;
using System.Threading;
using System;
using System.IO.Ports;
using WpfMudBlazor.Models;

namespace WpfMudBlazor.Services;
public class SerialCommunication : ISerialCommunication, IDisposable
{
    SerialPort serial = new SerialPort();
    public event EventHandler<string> SerialConnected;
    public event EventHandler<string> SerialDisconnected;
    public event EventHandler<string> SerialChanged;

    public bool IsConnected { get; set; }


    public SerialCommunication()
    {
    }

    public void SerialCmdSend(char data)
    {
        if (serial.IsOpen)
        {
            byte[] h = new byte[1 * sizeof(char)];
            char[] arr = { data };
            Buffer.BlockCopy(arr, 0, h, 0, h.Length);

            serial.Write(arr, 0, 1);
            Thread.Sleep(1);
            SerialChanged?.Invoke(this, $"SENT {(int)data} char properly on serial port");
        }
        else
        {
            SerialChanged?.Invoke(this, $"Failed to SEND  {(int)data} char. Serial port closed");
        }
    }
    public void SerialCmdSend(string data)
    {
        if (serial.IsOpen)
        {
            try
            {
                // Send the binary data out the port
                byte[] hexstring = Encoding.ASCII.GetBytes(data);
                //There is a intermitant problem that I came across
                //If I write more than one byte in succesion without a
                //delay the PIC i'm communicating with will Crash
                //I expect this id due to PC timing issues ad they are
                //not directley connected to the COM port the solution
                //Is a ver small 1 millisecound delay between chracters
                foreach (byte hexval in hexstring)
                {
                    byte[] _hexval = new byte[] { hexval }; // need to convert byte to byte[] to write
                    serial.Write(_hexval, 0, 1);
                    Thread.Sleep(1);
                }
                SerialChanged?.Invoke(this, $"SENT {data} properly on serial port");
            }
            catch (Exception ex)
            {
                SerialChanged?.Invoke(this, $"Failed to SEND {data} {ex}");
            }
        }
        else
        {
            SerialChanged?.Invoke(this, $"Failed to SEND  {data}. Serial port closed");
        }
    }

    public void ConnectToSerial(string portName, int baudRate)
    {
        try
        {
            //Sets up serial port
            serial.PortName = portName;
            serial.BaudRate = baudRate;
            serial.Handshake = System.IO.Ports.Handshake.None;
            serial.Parity = Parity.None;
            serial.DataBits = 8;
            serial.StopBits = StopBits.One;
            serial.ReadTimeout = 200;
            serial.WriteTimeout = 50;
            serial.Open();

            serial.DataReceived += Serial_DataReceived;

            SerialConnected?.Invoke(this, $"Port open on {portName} and baud rate {baudRate}.");
            IsConnected = true;
        }
        catch (Exception ex)
        {
            SerialChanged?.Invoke(this, $"Error opening serial port: {ex.Message}");
        }
    }

    public void Disconnect()
    {
        IsConnected = false;
        serial.DataReceived -= Serial_DataReceived;
        serial.Close();
        SerialDisconnected?.Invoke(this, $"Disconnected");
    }

    private void Serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        var receivedData = serial.ReadExisting();
        // Send notification to frontend
        SerialChanged?.Invoke(this, receivedData);
    }

    internal void SetName(string requestData)
    {
        serial.PortName = requestData;
    }

    internal void SetBaudRate(int requestData)
    {
        serial.BaudRate = requestData;
    }

    public void Dispose()
    {
        serial.Dispose();
    }
}
