using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Battleship.Commands
{
    public class FireShotPlayerCommand : IPlayerCommand
    {

        public readonly Coordinate Coordinate;

        public FireShotPlayerCommand(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
