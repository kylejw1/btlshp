using Battleship.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class Coordinate
    {
        public static readonly int RowCount = ConfigVariables.GridRows;
        public static readonly int ColCount = ConfigVariables.GridCols;

        public int Row { get; private set; }
        public int Col { get; private set; }

        private static bool ValidColumn(char col, out string message)
        {
            col = Char.ToUpperInvariant(col);
            var colNum = col - 'A';

            if (colNum < 0 || colNum >= ColCount)
            {
                message = string.Format("Column outside legal range A-{0}", 'A' + ColCount - 1);
                return false;
            }

            message = string.Empty;
            return true;
        }

        private static int GetColumnInt(char column)
        {
            column = Char.ToUpperInvariant(column);
            var columnInt = column - 'A';

            return columnInt;
        }

        private static char GetColumnChar(int col)
        {
            return (char)(col + 'A');
        }

        public static bool TryParse(string rowString, string colString, out string message, out Coordinate coordinate)
        {
            coordinate = null;
            message = string.Empty;

            if (string.IsNullOrWhiteSpace(rowString) || string.IsNullOrWhiteSpace(colString))
            {
                message = "You must enter a value for row and column";
                return false;
            }

            rowString = rowString.Trim();
            colString = colString.Trim();

            int row;
            if (!int.TryParse(rowString, out row))
            {
                message = "Row invalid: " + rowString;
                return false;
            }

            // Row numbers input from users are 1-indexed
            if (row < 1 || row > RowCount)
            {
                message = string.Format("Row outside legal range 1-{0}", RowCount);
                return false;
            }

            char col = colString.First();
            // Col ints are zero-indexed
            var colInt = GetColumnInt(col);
            if (colInt < 0 || colInt >= ColCount)
            {
                message = string.Format("Col outside legal range A-{0}", GetColumnChar(ColCount - 1));
                return false;
            }

            var coord = new Coordinate();
            coord.Row = row - 1;
            coord.Col = colInt;
            coordinate = coord;

            return true;
        }
    }
}
