using System;

namespace Battleship.Logging
{
    interface ILogger
    {
        void Info(string message, Exception ex);
        void Warn(string message, Exception ex);
        void Error(string message, Exception ex);
    }
}
