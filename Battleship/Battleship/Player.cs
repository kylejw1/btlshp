using Battleship.PlayerInterface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class Player
    {
        public readonly string Name;

        private readonly GameBoard _gameBoard = new GameBoard();
        private IPlayerInterface _playerInterface;
        private Ship _ship;

        public Player(string name, IPlayerInterface commandProvider)
        {
            Name = name;
            _playerInterface = commandProvider;
        }

        public bool ShipSunk
        {
            get
            {
                if (null == _ship)
                {
                    return false;
                }
                return _ship.Points.All(p => _gameBoard.GetCell(p).State == CellState.Hit);
            }
        }

        public void Fire(Player target)
        {
            bool fireComplete = false;
            while (!fireComplete)
            {
                // Get coord
                var point = _playerInterface.GetFiringCoordinate(this);

                // Check if coord legal on target (in rectangle)
                if (!target._gameBoard.EnclosingRectangle.Contains(point))
                {
                    _playerInterface.DisplayError("Firing coordinate out of bounds.  Try again.");
                    continue;
                }

                // Check if coord has not been fired on
                var cell = target._gameBoard.GetCell(point);
                if (cell.State != CellState.Pristine)
                {
                    _playerInterface.DisplayError("Cell has already been fired upon.  Try again.");
                    continue;
                }

                cell.State = target._ship.Points.Any(p => p.Equals(point)) ? CellState.Hit : CellState.Miss;

                _playerInterface.NotifyFireResult(cell.State);

                fireComplete = true;
            }
        }

        public void PlaceShip()
        {
            bool shipPositionValid = false;
            Ship ship = null;
            while(!shipPositionValid)
            {
                ship = _playerInterface.GetPlayerShip(this);

                if (!ship.Points.All(p => _gameBoard.EnclosingRectangle.Contains(p)))
                {
                    _playerInterface.DisplayError("Ship location out of bounds.  Try again.");
                    continue;
                }

                shipPositionValid = true;
            }
            _ship = ship;
        }

    }
}
