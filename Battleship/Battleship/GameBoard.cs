using Battleship.Config;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Battleship
{
    public class GameBoard
    {
        private Cell [,] _grid = new Cell[ConfigVariables.GridCols, ConfigVariables.GridRows];
        public readonly Rectangle EnclosingRectangle = new Rectangle(0, 0, ConfigVariables.GridCols, ConfigVariables.GridRows);

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

        public Cell GetCell(Point coordinates)
        {
            try
            {
                return _grid[coordinates.X, coordinates.Y];
            } 
            catch (Exception ex)
            {
                // TODO: Log
                return null;
            }
        }

    }
}