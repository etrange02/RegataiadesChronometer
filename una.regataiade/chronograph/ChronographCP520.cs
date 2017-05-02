using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace una.regataiade.chronograph
{
    public class ChronographCP520 : AbstractChronograph
    {
        public override string VersionName
        {
            get { return "CP520"; }
        }

        public override int GetBaudRate()
        {
            return 9600;
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

            if (command != 'T') return raceTime;
            var content = message.Substring(1);

            var orderNumber = content.Substring(7, 3);
            raceTime.Order = int.Parse(orderNumber);
            string canal = content.Substring(12, 1);

            //'Canal.Text = Canal
            //var time = content.Substring(4, 6) + "." + content.Substring(10, 3);
            var time = content.Substring(14, 2) + content.Substring(17, 2) + content.Substring(20, 9);
            //time = string.Format("{0:00:00:00.000}", time);
            time = time.Replace('.', Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
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
            while (serialPort.ReadBufferSize > 0)// '** Exécute tant que le buffer série en plein
            {
                switch (IdTrame)// '** Affiche les résultats dans la liste selon la fonction active
                {
                    case "A":
                    case "S":
                        break;
                    default:
                        MsgBox("Ce chronomètre n'est pas en mode (Split)" & Chr(13) & "ou ce n'est pas un TAG HEUER CP520", MsgBoxStyle.Critical, "Erreur");
                        M_Chrono_com_Click(M_Chrono_com.Item((1)), New System.EventArgs());
                        Exit Sub;
                        break;
                }
            }
            */
            return raceTime;
        }
    }
}
