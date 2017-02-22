using Battleship.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Text.RegularExpressions;
using Battleship.Config;

namespace Battleship.PlayerInterface
{
    public class TextPlayerInterface : IPlayerInterface
    {
        private TextReader _input;
        private TextWriter _output;

        /// <summary>
        /// Game configuration provider based on text streams.  Closure of streams is the responsibility of the caller.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public TextPlayerInterface(TextReader input, TextWriter output)
        {
            if (null == input || null == output)
            {
                throw new ArgumentNullException("Both input reader and output writer must be defined");
            }

            _input = input;
            _output = output;
        }

        public Point GetFiringCoordinate(Player shooter)
        {
            bool validPoint = false;
            Point point = new Point();
            int tries = 0;

            while (!validPoint)
            {
                if (tries++ > ConfigVariables.MaxInputAttempts)
                {
                    throw new Exception("Exceeded max attempts.");
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

        public Ship GetPlayerShip(Player player)
        {
            Ship ship = null;
            int tries = 0;

            while (null == ship)
            {
                if (tries++ > ConfigVariables.MaxInputAttempts)
                {
                    throw new Exception("Exceeded max attempts.");
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

        private bool TryParsePoint(string pointStr, out Point point)
        {
            if (string.IsNullOrWhiteSpace(pointStr))
            {
                point = new Point();
                return false;
            }

            var match = Regex.Match(pointStr, @"^(?<col>[\w])(?<row>[\d])$");
            if (!match.Success)
            {
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
