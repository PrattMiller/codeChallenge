using System;

namespace PME
{
    public interface IClock : IRunnable, INow 
    {

        DateTime StartTimestamp
        {
            get;
        }

        TimeSpan Elapsed
        {
            get;
        }
        
    }
}

