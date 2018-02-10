using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using una.regataiade;

namespace Chronometer.ViewModel
{
    public class RaceTimeViewModel : ViewModelBase
    {

        public RaceTimeViewModel(RaceTime raceTime)
        {
            RaceTime = raceTime;

            if (IsInDesignMode)
            {
                RaceTime = new RaceTime
                {
                    Order = 3,
                    Departure = "00:01:20.6"
                };
            }
        }

        public RelayCommand<RaceTimeViewModel> Resend { get; set; }

        public RaceTime RaceTime { get; set; }
        public string Departure => RaceTime.Departure;
        public string Arrival => RaceTime.Arrival;
        public int Order => RaceTime.Order;

        private bool _saved;
        public bool Saved
        {
            get { return _saved; }
            set { Set(ref _saved, value); }
        }
    }
}
