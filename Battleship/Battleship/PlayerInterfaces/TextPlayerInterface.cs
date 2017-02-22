using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Text.RegularExpressions;
using Battleship.Config;
using Battleship.Logging;

namespace Battleship.PlayerInterface
{
    public class TextPlayerInterface : IPlayerInterface
    {
        private TextReader _input;
        private TextWriter _output;
        ILogger logger = Resolver.Resolve<ILogger>();

        /// <summary>
        /// Get player moves from a text interface.  This may be the console, or other source
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public TextPlayerInterface(TextReader input, TextWriter output)
        {
            if (null == input || null == output)
            {
                throw new ArgumentNullException("TextPlayerInterface :: ctor :: Both input reader and output writer must be defined");
            }

            _input = input;
            _output = output;
        }

        /// <summary>
        /// Request a firing coordinate from this player
        /// </summary>
        /// <param name="shooter"></param>
        /// <returns></returns>
        public Point GetFiringCoordinate(Player shooter)
        {
            bool validPoint = false;
            Point point = new Point();
            int tries = 0;

            while (!validPoint)
            {
                if (tries++ > ConfigVariables.MaxInputAttempts)
                {
                    throw new Exception("TextPlayerInterface :: GetFiringCoordinate :: Exceeded max attempts.");
                }

                _output.WriteLine("{0}, what are our firing coordinates?", shooter.Name);

                var pointStr = _input.ReadLine();
                if (!TryParsePoint(pointStr, out point))
                {
                    _output.WriteLine("Failed to parse firing coordinate.  Try again.");
                    continue;
                }

                validPoint = true;
            }

            return point; 
        }

        /// <summary>
        /// Request the players desired ship location
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public Ship GetPlayerShip(Player player)
        {
            Ship ship = null;
            int tries = 0;

            while (null == ship)
            {
                if (tries++ > ConfigVariables.MaxInputAttempts)
                {
                    throw new Exception("TextPlayerInterface :: GetPlayerShip :: Exceeded max attempts.");
                }

                _output.WriteLine("{0}, choose your location!  Example: A1 C1 (Ship length 3 squares vertical or horizontal)", player.Name);
                var locationStr = _input.ReadLine();

                try
                {
                    var split = Regex.Split(locationStr.Trim(), @"\s+");
                    if (split.Count() != 2)
                    {
                        _output.WriteLine("Format error.  Try again.");
                        continue;
                    }

                    Point startPoint;
                    if (!TryParsePoint(split[0], out startPoint))
                    {
                        _output.WriteLine("Failed to parse start point.  Try again.");
                        continue;
                    }
                    Point endPoint;
                    if (!TryParsePoint(split[1], out endPoint))
                    {
                        _output.WriteLine("Failed to parse end point.  Try again.");
                        continue;
                    }

                    ship = new Ship(startPoint, endPoint);
                }
                catch (Exception ex)
                {
                    _output.WriteLine("Unknown error.  Try again.");
                }
            }

            return ship;
        }

        /// <summary>
        /// Attempt to parse a coordinate from a text input
        /// </summary>
        /// <param name="pointStr"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private bool TryParsePoint(string pointStr, out Point point)
        {
            if (string.IsNullOrWhiteSpace(pointStr))
            {
                logger.Warn("TextPlayerInterface :: TryParsePoint :: Tried to parse null point");
                point = new Point();
                return false;
            }

            var match = Regex.Match(pointStr, @"^(?<col>[\w])(?<row>[\d])$");
            if (!match.Success)
            {
                logger.Warn("TextPlayerInterface :: TryParsePoint :: Failed regex match on input " + pointStr);
                point = new Point();
                return false;
            }

            char col = match.Groups["col"].Value.ToUpperInvariant().First();
            int colIndex = col - 'A';

            // Row comes in 1-indexed, so convert to 0-index
            int rowIndex = int.Parse(match.Groups["row"].Value) - 1;

            point = new Point(colIndex, rowIndex);
            return true;
        }

        public void DisplayError(string message)
        {
            _output.WriteLine(message);
        }

    }
}
