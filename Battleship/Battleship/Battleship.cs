using Battleship.Logging;
using Battleship.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Battleship
{
    class Battleship
    {
        static void Main(string[] args)
        {
            Resolver.RegisterObject(typeof(ILogger), new Log4NetLogger());
            Resolver.RegisterObject(typeof(IView), new ConsoleView());

            var logger = Resolver.Resolve<ILogger>();

            try {
                var view = Resolver.Resolve<IView>();

                view.SetState(GameState.MENU);
                Console.ReadLine();
                view.SetState(GameState.PLAY);
                Console.ReadLine();
                view.SetState(GameState.SUNK);

            }
            catch (LoggableException ex)
            {
                // Have an exception that will be in a parsable format
                logger.Error(ex.LogEvent);
                Console.WriteLine(ex.LogEvent);
            }
            catch (Exception ex)
            {
                // Have a regular exception.
                logger.Error(new LogEvent(ex));
            }
            Console.ReadLine();
            

        }
    }
}
