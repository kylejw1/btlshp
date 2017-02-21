using Battleship.Commands;
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

        private readonly GameBoard _gameBoard;
        private IEnumerable<Point> _firedShots = new List<Point>();
        private IPlayerInterface _commandProvider;
        private Ship _ship;

        public Player(string name, GameBoard gameBoard, IPlayerInterface commandProvider)
        {
            Name = name;
            _gameBoard = gameBoard;
            _commandProvider = commandProvider;
        }

        public bool ShipSunk
        {
            get
            {
                if (null == _ship)
                {
                    return false;
                }
                return _ship.Sunk;
            }
        }

        public void Fire(Player target)
        {
            var cmd = _commandProvider.CreateFireShotCommand(target._gameBoard);
            cmd.Execute();
        }

        public void PlaceShip()
        {
            var cmd = _commandProvider.CreatePlaceShipCommand(_gameBoard);
            cmd.Execute();
        }

    }
}
