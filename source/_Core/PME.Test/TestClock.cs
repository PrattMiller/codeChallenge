using System;

namespace PME.Test
{
    public class TestClock : Startable, IClock, IAcceleratedClock, INow 
    {
        // GOAL: Create a updatable live clock to simulate time change

        private decimal _accelerationFactor = 1;
        private DateTime _onStartTimestamp;

        private DateTime _now;
        private DateTime _nowTimestamp;

        public TestClock()
        {
        }

        protected override void OnStart()
        {
            if (_now == DateTime.MinValue)
            {
                _now = DateTime.UtcNow;
                _nowTimestamp = _now;
            }
            else
            {
                _nowTimestamp = DateTime.UtcNow;
            }

            _onStartTimestamp = _now;
        }

        protected override void OnStop()
        {
            _onStartTimestamp = DateTime.MinValue;
        }

        public DateTime StartTimestamp
        {
            get { return _onStartTimestamp; }
        }

        public decimal AccelerationFactor
        {
            get { return _accelerationFactor; }
            set { _accelerationFactor = value; }
        }

        public TimeSpan Elapsed
        {
            get
            {
                if (!IsRunning)
                {
                    return TimeSpan.Zero;
                }

                // NOTE: Take the real amount of time that has elapsed 
                // and multiply it by an acceleration factor

                var startTicks = _onStartTimestamp.Ticks;
                var diff = (this.Now - _onStartTimestamp).Ticks;

                var accelratedTicks = (long)Math.Round(diff * _accelerationFactor, 0);

                return TimeSpan.FromTicks(accelratedTicks);
            }
        }

        public DateTime Now
        {
            get
            {
                if (!IsRunning)
                {
                    return DateTime.MinValue;
                }

                // Now is a method of determining the distance between
                // the amount of time the NOW value was set...

                var distance = DateTime.UtcNow - _nowTimestamp;

                return _now.Add(distance);
            }
            set
            {
                _nowTimestamp = DateTime.UtcNow;

                _now = value;
            }
        }

    }
}

