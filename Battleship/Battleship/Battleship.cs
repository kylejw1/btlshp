using Battleship.Logging;
using Battleship.View;
using System;
using System.Linq;
using Battleship.Config;
using Battleship.PlayerInterface;
using System.Drawing;
using Battleship.Models;

namespace Battleship
{
    class Battleship
    {
        static void Main(string[] args)
        {
            // All resolved interface instances can easily be made configurable in a config file, or even at run time.  
            // Eg. for hypothetical network play, the console could be used to determine network configuration, host or client, etc. 
            // Then the game command provider would use some kind of network stream instead of std I/O.
            // Though a complete game would likely use a package such as castle windsor to achieve this.

            Resolver.RegisterObject(typeof(ILogger), new Log4NetLogger());
            var logger = Resolver.Resolve<ILogger>();
            logger.Info("Application Starting");

            Resolver.RegisterObject(typeof(IGameView), new ConsoleGameView());

            // Configuration provider could take the form of a UI+Controller, Web App, etc.  For this demo, it uses Console + Std I/O
            Resolver.RegisterObject(typeof(IConfigurationProvider), new TextConfiguratonProvider(Console.In, Console.Out));

            // Player command provider could take the form of an AI player, TCP socket for network play, UI+Controller, etc.  
            Resolver.RegisterObject(typeof(IPlayerInterface), new TextPlayerInterface(Console.In, Console.Out));

            try {
                // Resolve dependencies 
                var config = Resolver.Resolve<IConfigurationProvider>().GetConfiguration();
                IGameView view = Resolver.Resolve<IGameView>();
                IPlayerInterface playerInterface = Resolver.Resolve<IPlayerInterface>();

                var player1 = new Player(config.Player1Name);
                var player2 = new Player(config.Player2Name);

                view.Initialize(player1, player2);    
                view.Show();

                logger.Info(string.Format("Starting match between {0} and {1}", player1.Name, player2.Name));

                var ship1 = GetPlayerShip(player1, playerInterface);
                player1.SetShip(ship1);
                var ship2 = GetPlayerShip(player2, playerInterface);
                player2.SetShip(ship2);

                view.SetShip(player1, ship1.Points);
                view.SetShip(player2, ship2.Points);

                while (!player1.ShipSunk && !player2.ShipSunk)
                {
                    FireShot(player1, player2, playerInterface, view);
                    if (player2.ShipSunk)
                    {
                        break;
                    }

                    FireShot(player2, player1, playerInterface, view);
                    if (player1.ShipSunk)
                    {
                        break;
                    }
                }

                if (player1.ShipSunk)
                {
                    logger.Info(string.Format("{0} ship sunk", player1.Name));
                    view.SetSunk(player1);
                }
                else
                {
                    logger.Info(string.Format("{0} ship sunk", player2.Name));
                    view.SetSunk(player2);
                }
            }
            catch (Exception ex)
            {
                // Have a regular exception.
                logger.Error("Exception ocurred in Main :: ", ex);
            }
            Console.ReadLine();

            logger.Info("Application Exiting");
        }

        private static void FireShot(Player shooter, Player target, IPlayerInterface playerInterface, IGameView view)
        {
            var coord = GetShotCoordinates(shooter, playerInterface);
            shooter.FireShot(coord);
            var shotResult = target.IncomingShot(coord);
            view.SetFireResult(target, coord, shotResult);
        }

        private static Ship GetPlayerShip(Player player, IPlayerInterface playerInterface)
        {
            Ship ship = null;
            bool shipValid = false;
            int tries = 0;

            while (!shipValid)
            {
                if (tries++ > ConfigVariables.MaxInputAttempts)
                {
                    throw new Exception("GetPlayerShip Exceeded max attempts.");
                }

                ship = playerInterface.GetPlayerShip(player);

                if (!ship.Points.All(p => GameBoard.EnclosingRectangle.Contains(p)))
                {
                    playerInterface.DisplayError("Ship location out of bounds.  Try again.");
                    continue;
                }

                shipValid = true;
            }

            return ship;
        }

        private static Point GetShotCoordinates(Player player, IPlayerInterface playerInterface)
        {
            bool fireComplete = false;
            Point point = new Point();
            int tries = 0;
            while (!fireComplete)
            {
                if (tries++ > ConfigVariables.MaxInputAttempts)
                {
                    throw new Exception("GetShotCoordinates Exceeded max attempts.");
                }

                // Get coord
                point = playerInterface.GetFiringCoordinate(player);

                // Check if coord legal on target (in rectangle)
                if (!GameBoard.EnclosingRectangle.Contains(point))
                {
                    playerInterface.DisplayError("Firing coordinate out of bounds.  Try again.");
                    continue;
                }

                if (player.CoordinateAlreadyTried(point))
                {
                    playerInterface.DisplayError("Cell has already been fired upon.  Try again.");
                    continue;
                }

                fireComplete = true;
            }

            return point;
        }
    }
}
