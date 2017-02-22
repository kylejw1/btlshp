using Battleship.Config;
using Battleship.Logging;
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
        ILogger logger = Resolver.Resolve<ILogger>();

        public Player(string name)
        {
            Name = string.IsNullOrWhiteSpace(name) ? "Player" + blankNameToken++ : name;
        }

        /// <summary>
        /// Set player ship
        /// </summary>
        /// <param name="ship"></param>
        public void SetShip(Ship ship)
        {
            if (null == ship || null == ship.Points)
            {
                logger.Warn(Name + " attempted to set null or empty ship points");
                throw new ArgumentNullException("Ship points cannot be empty!");
            }

            foreach(var point in ship.Points)
            {
                _shipCells[point] = CellState.Pristine;
            }

            logger.Info(string.Format("Player {0} sets ship to ", Name, string.Join("-", ship.Points.Select(p => "(" + p.X + "," + p.Y + ")"))));
        }

        /// <summary>
        /// Check if this player's ship is sunk
        /// </summary>
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

        /// <summary>
        /// Fire a shot from this player
        /// </summary>
        /// <param name="coordinate"></param>
        public void FireShot(Point coordinate)
        {
            logger.Info(string.Format("{0} fires shot {1},{2}", Name, coordinate.X, coordinate.Y));
            _firedShots.Add(coordinate);
        } 

        /// <summary>
        /// Check if this player has already fired a shot at the requested location
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public bool CoordinateAlreadyTried(Point coordinate)
        {
            return _firedShots.Contains(coordinate);
        }

        /// <summary>
        /// Process an incoming shot and return the damage!
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        public CellState IncomingShot(Point coord)
        {
            if (!_shipCells.ContainsKey(coord))
            {
                logger.Info(string.Format("{0}: Incoming Shot missed :: {1},{2}", Name, coord.X, coord.Y));
                return CellState.Miss;
            }
            else
            {
                logger.Info(string.Format("{0}: Incoming Shot hit :: {1},{2}", Name, coord.X, coord.Y));
                _shipCells[coord] = CellState.Hit;
                return _shipCells[coord];
            }
        }
    }
}
