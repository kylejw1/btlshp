using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Logging
{
    interface ILogger
    {
        void Info(LogEvent logEvent);
        void Warn(LogEvent logEvent);
        void Error(LogEvent logEvent);
    }
}
