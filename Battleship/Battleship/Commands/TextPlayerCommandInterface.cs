using Battleship.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Commands
{
    public class TextPlayerCommandInterface : IPlayerInterface
    {
        private TextReader _input;
        private TextWriter _output;

        /// <summary>
        /// Game configuration provider based on text streams.  Closure of streams is the responsibility of the caller.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public TextPlayerCommandInterface(TextReader input, TextWriter output)
        {
            if (null == input || null == output)
            {
                throw new ArgumentNullException("Both input reader and output writer must be defined");
            }

            _input = input;
            _output = output;
        }

        public IPlayerCommand CreateFireShotCommand(GameBoard gameBoard)
        {
            _output.WriteLine("{0}, what are our firing coordinates?", gameBoard.PlayerName);

            Coordinate coordinate = ReadCoordinate();
            var cmd = new FireShotPlayerCommand(coordinate, gameBoard);

            return cmd;
        }

        public IPlayerCommand CreatePlaceShipCommand(GameBoard gameBoard)
        {
            _output.WriteLine("{0}, choose your location!", gameBoard.PlayerName);
            TextViewTemplates.GameBoard.ForEach(line => _output.WriteLine(line));

            _output.WriteLine("Enter first coordinate");
            var firstCoord = ReadCoordinate();

            Coordinate secondCoord = null;
            while (null == secondCoord)
            {
                _output.WriteLine("Enter second coordinate (ship length 3 squares vertical or horizontal)");
                secondCoord = ReadCoordinate();

                // TODO: Use ship class which will validate second coord as a legit ship start and end
            }

            return new PlaceShipPlayerCommand(gameBoard, new List<Coordinate>() { firstCoord, secondCoord, secondCoord });
        }

        public Coordinate GetFiringCoordinate(string playerName)
        {
            _output.WriteLine("{0}, what are our firing coordinates?", playerName);

            return ReadCoordinate();
        }

        private Coordinate ReadCoordinate()
        {
            Coordinate coordinate = null;
            string message = null;
            while (null == coordinate)
            {
                try
                {
                    // Output any errors from the previous loop
                    if (null != message)
                    {
                        _output.WriteLine(message + ".  Try again:");
                    }

                    _output.WriteLine("Column:");
                    var colString = _input.ReadLine().Trim();
                    _output.WriteLine("Row:");
                    var rowString = _input.ReadLine().Trim();

                    coordinate = new Coordinate();
                    coordinate.X = 0;
                    coordinate.Y = 1;
                    //Coordinate.TryParse(rowString, colString, out message, out coordinate);

                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    continue;
                }
            }

            return coordinate;
        }
    }
}
