using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace una.regataiade.chronograph
{
    public class SerialPortManager
    {
        private const int MaxPorts = 6;
        public string Port { get; set; }
        public SerialPort SerialPort { get; private set; }
        public bool IsOpened => SerialPort.IsOpen;

        public List<string> AvailablePorts
        {
            get
            {
                return new List<string>(SerialPort.GetPortNames());
            }
        }

        public SerialPortManager()
        {
            SerialPort = new SerialPort();
        }

        public bool Open(AbstractChronograph chronograph, string comPort)
        {
            Port = comPort;
            if (!AvailablePorts.Contains(Port))
                return SerialPort.IsOpen;

            if (SerialPort.IsOpen)
                SerialPort.Close();

            if (string.IsNullOrEmpty(Port) || !(Port.ToLower()).StartsWith("com"))
                return SerialPort.IsOpen;

            try
            {
                SerialPort.PortName = Port;
                SerialPort.BaudRate = chronograph.GetBaudRate();
                SerialPort.DataBits = chronograph.GetDataBits();
                SerialPort.StopBits = chronograph.GetStopBits();
                SerialPort.Parity = chronograph.GetParity();
                SerialPort.NewLine = chronograph.GetNewLineSeparator();
                SerialPort.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return SerialPort.IsOpen;
        }

        public void Close()
        {
            SerialPort.Close();
        }
    }
}
