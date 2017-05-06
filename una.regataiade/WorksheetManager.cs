using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace una.regataiade
{
    public class WorksheetManager
    {
        private Microsoft.Office.Interop.Excel.Worksheet _sheet;
        private Microsoft.Office.Interop.Excel.Workbook _workbook;
        public string File { get; private set; }
        public ObservableCollection<string> Worksheets { get; private set; } = new ObservableCollection<string>();

        private int _offset = 0;
        public int Offset
        {
            get { return _offset; }
            set
            {
                if (value >= 0)
                    _offset = value;
            }
        }

        public void Open(string filepath)
        {
            if (string.IsNullOrEmpty(filepath))
                return;

            Close();

            Microsoft.Office.Interop.Excel.Application app;

            try
            {
                var filename = Path.GetFileName(filepath);
                app = (Microsoft.Office.Interop.Excel.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application");
                _workbook = app.Workbooks.Item[filename];
                File = filepath;
            }
            catch (Exception ex)
            {
                try
                {
                    //Start Excel and get Application object.
                    app = new Microsoft.Office.Interop.Excel.Application();
                    app.Visible = true;

                    var workbooks = app.Workbooks;
                    _workbook = app.Workbooks.Open(filepath);

                    app.UserControl = true;
                    File = filepath;
                }
                catch (Exception)
                {
                    _workbook = null;
                }
            }

            Worksheets.Clear();
            if (_workbook != null)
            {
                var enumerator = _workbook?.Worksheets.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var sheet = (Microsoft.Office.Interop.Excel.Worksheet)enumerator.Current;
                    Worksheets.Add(sheet.Name);
                }
            }
        }

        public bool SelectSheet(string sheet)
        {
            _sheet = _workbook?.Worksheets.Item[sheet];
            return _sheet != null;
        }

        public bool IsOpen()
        {
            return _workbook != null && _sheet != null;
        }

        public void Close()
        {
            File = "";
            Worksheets.Clear();
            _workbook?.Close();
            _sheet = null;
        }

        public void AddRaceTime(RaceTime raceTime)
        {
            if (!IsOpen())
                return;
            
            try
            {
                if (raceTime.Departure != null)
                {
                    _sheet.Cells[Offset + raceTime.Order, 1] = raceTime.Order;
                    _sheet.Cells[Offset + raceTime.Order, 2] = raceTime.Departure;
                }
                else if (raceTime.Arrival != null)
                {
                    _sheet.Cells[Offset + raceTime.Order, 3] = raceTime.Order;
                    _sheet.Cells[Offset + raceTime.Order, 4] = raceTime.Arrival;
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}
