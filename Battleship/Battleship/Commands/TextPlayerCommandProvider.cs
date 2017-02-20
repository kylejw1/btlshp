using Battleship.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Commands
{
    public class TextPlayerCommandProvider : IPlayerCommandProvider
    {
        private TextReader _input;
        private TextWriter _output;
        private Player _player;

        /// <summary>
        /// Game configuration provider based on text streams.  Closure of streams is the responsibility of the caller.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public TextPlayerCommandProvider(Player player, TextReader input, TextWriter output)
        {
            if (null == input || null == output)
            {
                throw new ArgumentNullException("Both input reader and output writer must be defined");
            }

            _input = input;
            _output = output;
        }

        public IPlayerCommand CreateFireShotCommand()
        {
            _output.WriteLine("{0}, what are our firing coordinates?", _player.Name);

            Coordinate coordinate = ReadCoordinate();
            var cmd = new FireShotPlayerCommand(coordinate);

            return cmd;
        }

        public IPlayerCommand CreatePlaceShipCommand()
        {
            _output.WriteLine("{0}, choose your location!", _player.Name);
            TextViewTemplates.GameBoard.ForEach(line => _output.WriteLine(line));

            _output.WriteLine("Enter first coordinate");
            var firstCoord = ReadCoordinate();

            Coordinate secondCoord = null;
            while(null == secondCoord)
            {
                _output.WriteLine("Enter second coordinate (ship length 3 squares vertical or horizontal)");
                secondCoord = ReadCoordinate();

                if (secondCoord.Row != firstCoord.Row && secondCoord.Col != firstCoord.Col) {
                    _output.WriteLine("Second coordinate must be colinear to first!");
                    continue;
                }

                var rowDist = Math.Abs(secondCoord.Row - firstCoord.Row);
                var colDist = Math.Abs(secondCoord.Col - firstCoord.Col);
                if (rowDist != 3 && colDist != 3)
                {
                    _output.WriteLine("Ship length must be 3 squares!");
                    continue;
                }
            }

            

            return cmd;
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

                    Coordinate.TryParse(rowString, colString, out message, out coordinate);

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
