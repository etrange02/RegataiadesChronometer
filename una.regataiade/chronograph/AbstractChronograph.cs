using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace una.regataiade.chronograph
{
    public abstract class AbstractChronograph
    {
        public string GetName()
        {
            return "Chronomètre " + GetBrand() + " " + VersionName + " (" + GetBaudRate() + ", " + GetParity() + ", " + GetDataBits() + ", " + GetStopBits() + ")";
        }
        public abstract string VersionName { get; }
        public abstract int GetBaudRate();
        public abstract int GetDataBits();
        public abstract StopBits GetStopBits();
        public abstract Parity GetParity();
        public abstract string GetBrand();
        public abstract string GetNewLineSeparator();
        public abstract RaceTime Interpret(SerialPort serialPort);
    }
}
