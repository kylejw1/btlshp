using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class Coordinate
    {
        public readonly int Row;
        public readonly int Col;

        public Coordinate(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}
