using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public enum CellState
    {
        Pristine,
        Miss,
        Hit
    }

    public class Cell
    {
        public CellState State = CellState.Pristine;
    }
}
