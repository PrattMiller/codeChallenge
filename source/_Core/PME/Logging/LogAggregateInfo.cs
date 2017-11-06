using System;

namespace PME.Logging
{
    public class LogAggregateInfo
    {

        public int Level
        {
            set
            {
                LogLevel = (LogLevel)value ?? LogLevel.All;
            }
        }

        public LogLevel LogLevel { get; set; }

        public Guid? ApplicationId { get; set; }

        public string Source { get; set; }

        public int Count { get; set; }
    }
}
