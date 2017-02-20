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

            // Player command provider could take the form of a TCP socket for network play, UI+Controller, etc.  
            Resolver.RegisterObject(typeof(IPlayerCommandProvider), new TextPlayerCommandProvider(Console.In, Console.Out));

            try {

                var config = Resolver.Resolve<IConfigurationProvider>()
                    .GetConfiguration();

                var board1 = new GameBoard(config.Player1Name);
                var board2 = new GameBoard(config.Player2Name);

                
                IPlayerCommandProvider playerCommandProvider = new TextPlayerCommandProvider(Console.In, Console.Out);

                playerCommandProvider.CreatePlaceShipCommand(board1).Execute();
                playerCommandProvider.CreatePlaceShipCommand(board2).Execute();

                while(!board1.Sunk && !board2.Sunk)
                {
                    playerCommandProvider.CreateFireShotCommand(board1).Execute();
                    if (board2.Sunk)
                    {
                        break;
                    }
                    playerCommandProvider.CreateFireShotCommand(board2).Execute();
                    if (board1.Sunk)
                    {
                        break;
                    }
                }

                var winner = board1.Sunk ? board2 : board1;

                // TODO:  Proper view instead of console write
                Console.Out.Write("{0} wins!", winner.PlayerName);

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
