using System;

namespace Battleship.Logging
{
    interface ILogger
    {
        void Info(string message, Exception ex = null);
        void Warn(string message, Exception ex = null);
        void Error(string message, Exception ex = null);
    }
}
