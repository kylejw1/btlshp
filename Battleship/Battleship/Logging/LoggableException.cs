using System;
using System.Runtime.Serialization;

namespace Battleship.Logging
{
    public class LoggableException : Exception, ISerializable
    {
        public LogEvent LogEvent { get; private set; }

        public LoggableException(LogEvent logEvent)
        {
            LogEvent = logEvent;
        }
    }
}
