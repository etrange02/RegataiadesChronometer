using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using una.regataiade.chronograph;

namespace una.regataiade
{
    // Vitesse de transmission : 2400 bauds
	// Parité : None
	// Data : 8 bit
	// Stop bit : 1
    public class ChronographCP502 : AbstractChronograph
    {
        public override string VersionName
        {
            get { return "CP502"; }
        }

        public override int GetBaudRate()
        {
            return 2400;
        }

        public override string GetBrand()
        {
            return "TAG HEUER";
        }

        public override int GetDataBits()
        {
            return 8;
        }

        public override Parity GetParity()
        {
            return Parity.None;
        }

        public override StopBits GetStopBits()
        {
            return StopBits.One;
        }

        public override string GetNewLineSeparator()
        {
            return "\r";
        }

        public override RaceTime Interpret(SerialPort serialPort)
        {
            RaceTime raceTime = new RaceTime();
            var message = serialPort.ReadLine();
            int command = message[0];

            if (command == 'A' 
                || command == 'T' 
                || message.Length != 14)
                return raceTime;

            var content = message.Substring(1);

            var orderNumber = content.Substring(0, 3);
            raceTime.Order = int.Parse(orderNumber);
            string canal = content.Substring(3, 1);

            //'Canal.Text = Canal
            var time = content.Substring(4, 6) + Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) + content.Substring(10, 3);
            time = string.Format("{0:00:00:00.000}", double.Parse(time));

            if (canal == "1")
            {
                raceTime.Departure = time;
            }
            else if (canal == "2")
            {
                raceTime.Arrival = time;
            }
                        
            /*
            switch (command)
            {
                case 0xFF: // 255
                    RS232.InputLen = 14;
                    inputSize = 14;
                    break;
                case 0xF5: // 245
                    RS232.InputLen = 10;
                    inputSize = 10;
                    break;
                case 0xF9: // 249
                    RS232.InputLen = 2;
                    inputSize = 2;
                    break;
                case 0xFA: // 250
                    RS232.InputLen = 14;
                    inputSize = 14;
                    break;
                case 'A':
                    TE = RS232.Input;
                    MsgBox("Ce chronomètre n'est pas un TAG HEUER CP502", MsgBoxStyle.Critical, "Erreur");
                    M_Chrono_com_Click(M_Chrono_com.Item((1)), New System.EventArgs());
                    return;
                case 'T':
                    TE = RS232.Input
                    MsgBox("Ce chronomètre n'est pas un TAG HEUER CP502", MsgBoxStyle.Critical, "Erreur")
                    M_Chrono_com_Click(M_Chrono_com.Item((1)), New System.EventArgs())
                    return;
                default:
                    inputSize = 0;
                    break;
            }
            */
            return raceTime;
        }
    }
}
