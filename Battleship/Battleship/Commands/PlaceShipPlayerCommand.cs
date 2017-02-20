using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Commands
{
    public class PlaceShipPlayerCommand : IPlayerCommand
    {
        private List<Coordinate> _shipLocation;
        private readonly GameBoard _gameBoard;

        public PlaceShipPlayerCommand(GameBoard gameBoard, List<Coordinate> shipLocation)
        {
            _shipLocation = shipLocation;
            _gameBoard = gameBoard;
        }

        public void Execute()
        {
            _gameBoard.SetShip(_shipLocation);
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
