using Battleship.Logging;
using Battleship.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Battleship.Config;
using Battleship.Commands;

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
            Resolver.RegisterObject(typeof(IPlayerInterface), new TextPlayerCommandInterface(Console.In, Console.Out));

            try {

                var config = Resolver.Resolve<IConfigurationProvider>()
                    .GetConfiguration();

                IPlayerInterface playerCommandInterface = new TextPlayerCommandInterface(Console.In, Console.Out);
                var board1 = new GameBoard();
                var board2 = new GameBoard();
                var player1 = new Player(config.Player1Name, board1, playerCommandInterface);
                var player2 = new Player(config.Player2Name, board2, playerCommandInterface);

                player1.PlaceShip();
                player2.PlaceShip();

                while (!board1.Sunk && !board2.Sunk)
                {
                    player1.Fire(player2);
                    if (board2.Sunk)
                    {
                        break;
                    }

                    player2.Fire(player1);
                    if (board1.Sunk)
                    {
                        break;
                    }
                }

                var winner = board1.Sunk ? player2 : player1;

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
