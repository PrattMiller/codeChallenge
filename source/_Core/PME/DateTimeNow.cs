using System;

namespace PME
{
    internal class DateTimeNow : INow 
    {

        public DateTime Now
        {
            get { return DateTime.UtcNow; }
        }

    }
}

