using log4net;
using log4net.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Logging
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(Battleship));

        public void Error(LogEvent logEvent)
        {
            logger.Error(logEvent);
        }

        public void Info(LogEvent logEvent)
        {
            logger.Info(logEvent);
        }

        public void Warn(LogEvent logEvent)
        {
            logger.Warn(logEvent);
        }
    }
}
