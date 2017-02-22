using Battleship.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Battleship
{
    public class Player
    {
        public readonly string Name;

        private List<Point> _firedShots = new List<Point>();
        private Dictionary<Point, CellState> _shipCells = new Dictionary<Point, CellState>();
        private static int blankNameToken = 1;

        public Player(string name)
        {
            Name = string.IsNullOrWhiteSpace(name) ? "Player" + blankNameToken++ : name;
        }

        public void SetShip(Ship ship)
        {
            if (null == ship || null == ship.Points)
            {
                //TODO: log?
                throw new ArgumentNullException("Ship points cannot be empty!");
            }

            foreach(var point in ship.Points)
            {
                _shipCells[point] = CellState.Pristine;
            }
        }

        public bool ShipSunk
        {
            get
            {
                if (null == _shipCells || !_shipCells.Any())
                {
                    return false;
                }
                return _shipCells.All(c => c.Value == CellState.Hit);
            }
        }

        public void FireShot(Point coordinate)
        {
            _firedShots.Add(coordinate);
        } 

        public bool CoordinateAlreadyTried(Point coordinate)
        {
            return _firedShots.Contains(coordinate);
        }

        public CellState IncomingShot(Point coord)
        {
            if (!_shipCells.ContainsKey(coord))
            {
                return CellState.Miss;
            }
            else
            {
                _shipCells[coord] = CellState.Hit;
                return _shipCells[coord];
            }
        }
    }
}
