using Battleship.Logging;
using Battleship.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Battleship.Config;
using Battleship.PlayerInterface;

namespace Battleship
{
    class Battleship
    {
        static void Main(string[] args)
        {
            // All resolved interface instances can easily be made configurable in a config file, or even at run time.  
            // Eg. for hypothetical network play, the console could be used to determine network configuration, host or client, etc. 
            // Then the game command provider would use some kind of network stream instead of std I/O.
            Resolver.RegisterObject(typeof(ILogger), new Log4NetLogger());
            var logger = Resolver.Resolve<ILogger>();

            Resolver.RegisterObject(typeof(IView), new ConsoleView());

            // Configuration provider could take the form of a UI+Controller, Web App, etc.  For this demo, it uses Console + Std I/O
            Resolver.RegisterObject(typeof(IConfigurationProvider), new TextConfiguratonProvider(Console.In, Console.Out));

            // Player command provider could take the form of an AI player, TCP socket for network play, UI+Controller, etc.  
            Resolver.RegisterObject(typeof(IPlayerInterface), new TextPlayerInterface(Console.In, Console.Out));

            try {

                var config = Resolver.Resolve<IConfigurationProvider>()
                    .GetConfiguration();

                IPlayerInterface playerInterface = new TextPlayerInterface(Console.In, Console.Out);
                var player1 = new Player(config.Player1Name, playerInterface);
                var player2 = new Player(config.Player2Name, playerInterface);

                player1.PlaceShip();
                player2.PlaceShip();

                while (!player1.ShipSunk && !player2.ShipSunk)
                {
                    player1.Fire(player2);
                    if (player2.ShipSunk)
                    {
                        break;
                    }

                    player2.Fire(player1);
                    if (player1.ShipSunk)
                    {
                        break;
                    }
                }

                var winner = player1.ShipSunk ? player2 : player1;

                // TODO:  Proper view instead of console write
                Console.Out.Write("{0} wins!", winner.Name);

            }
            catch (LoggableException ex)
            {
                // TODO: Seriously re think this strange exception type

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
