using System.Drawing;

namespace Battleship.PlayerInterface
{
    public interface IPlayerInterface
    {
        Ship GetPlayerShip(Player player);
        Point GetFiringCoordinate(Player shooter);
        void DisplayError(string message);
    }
}
