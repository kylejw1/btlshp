using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Commands
{
    public interface IPlayerCommand
    {
        void Execute();

        // Undo could be implemented to allow displaying a replay of the game
        void Undo();
    }
}
