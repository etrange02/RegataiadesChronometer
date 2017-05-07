using System;

namespace una.regataiade.excel
{
    public class RaceTimeNotAddedException : Exception
    {
        private RaceTime _raceTime;
        public RaceTimeNotAddedException(RaceTime raceTime)
        {
            _raceTime = raceTime;
        }

        public override string Message
        {
            get
            {
                if (_raceTime == null)
                    return "No time received";
                if (_raceTime.Departure != null)
                    return $"Time not set: Departure-({_raceTime.Order} - {_raceTime.Departure})";
                else if (_raceTime.Arrival != null)
                    return $"Time not set: Arrival-({_raceTime.Order} - {_raceTime.Arrival})";
                return $"Error on set {_raceTime.Order}, no time data";
            }
        }
    }
}
