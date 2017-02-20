using Battleship.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship
{
    public class GameBoard
    {
        public string PlayerName { get; private set; }
        private Cell [,] _grid = new Cell[ConfigVariables.GridCols, ConfigVariables.GridRows];
        private List<Cell> _ship;

        public GameBoard(string playerName)
        {
            PlayerName = playerName;
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

        public void SetShip(List<Coordinate> coordinates)
        {

            // TODO: Belongs in ship class. game board shouldn't know about ship other than that it should fit in grid

            if (coordinates.Count != 3)
            {
                throw new ArgumentOutOfRangeException("Ship must be exactly three cells");
            }

            if (!Coordinate.ColinearAboutAxes(coordinates))
            {
                throw new ArgumentException("Ship coordinates must be colinear about either X or Y axis");
            }

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







    }
}