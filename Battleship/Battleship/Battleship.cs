using Battleship.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Battleship
    {
        static void Main(string[] args)
        {
            Resolver.RegisterObject(typeof(ILogger), new Log4NetLogger());

            var logger = Resolver.Resolve<ILogger>();

            dynamic k = new LogEvent();
            k.Kyle = true;
            k.Not = null;
            logger.Info(k);
        }
    }
}
