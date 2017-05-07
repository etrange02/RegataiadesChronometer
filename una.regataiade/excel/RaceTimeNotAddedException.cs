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
                if (!string.IsNullOrEmpty(_raceTime.Departure))
                    return $"Time not set: Departure-({_raceTime.Order} - {_raceTime.Departure})";
                return $"Time not set: Arrival-({_raceTime.Order} - {_raceTime.Arrival})";
            }
        }
    }
}
