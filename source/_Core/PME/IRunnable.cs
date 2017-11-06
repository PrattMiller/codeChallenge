using System;

namespace PME
{
    public interface IRunnable
    {
        bool IsRunning
        {
            get;
        }

        void Start();

        void Stop();
    }
}

