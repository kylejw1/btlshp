using Battleship.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship
{
    public class GameBoard
    {
        private Cell [,] _grid = new Cell[ConfigVariables.GridCols, ConfigVariables.GridRows];
        private List<Cell> _ship;

        public GameBoard()
        {
            for (int row = 0; row < ConfigVariables.GridRows; row++)
            {
                for (int col = 0; col < ConfigVariables.GridCols; col++)
                {
                    _grid[row, col] = new Cell();
                }
            }
        }

        public bool Sunk
        {
            get
            {
                if (null == _ship || !_ship.Any())
                {
                    return false;
                }

                return _ship.All(c => c.State == CellState.Hit);
            }
        }

        public void SetShip(Ship ship)
        {
            
            if (coordinates.Any(c => c.X < 0 || c.X >= ConfigVariables.GridCols) ||
                coordinates.Any(c => c.Y < 0 || c.Y >= ConfigVariables.GridRows))
            {
                throw new ArgumentOutOfRangeException("Ship coordinates must be confined within the grid");
            }

            _ship = new List<Cell>();
            foreach(var coord in coordinates)
            {
                _ship.Add(_grid[coord.X, coord.Y]);
            }
        }

        public void IncomingShot(Coordinate coordinate)
        {
            var cell = _grid[coordinate.X, coordinate.Y];
            var newState = _ship.Contains(cell) ? CellState.Hit : CellState.Miss;
            cell.State = newState;
        }







    }
}