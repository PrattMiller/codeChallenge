using System;

namespace PME
{
    public interface IAcceleratedClock : IClock
    {


        decimal AccelerationFactor
        {
            get;
            set;
        }

    }
}

