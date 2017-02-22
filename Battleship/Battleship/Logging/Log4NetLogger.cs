using log4net;
using System;

namespace Battleship.Logging
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(Battleship));

        public void Error(string message, Exception ex=null)
        {
            logger.Error(string.Format("{0}{1}{2}", message, null == ex ? "" : " :: ", null == ex ? "" : ex.Message));
        }

        public void Info(string message, Exception ex = null)
        {
            logger.Info(string.Format("{0}{1}{2}", message, null == ex ? "" : " :: ", null == ex ? "" : ex.Message));
        }

        public void Warn(string message, Exception ex = null)
        {
            logger.Warn(string.Format("{0}{1}{2}", message, null == ex ? "" : " :: ", null == ex ? "" : ex.Message));
        }
    }
}
