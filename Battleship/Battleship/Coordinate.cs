using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class Coordinate
    {
        public int X;
        public int Y;

        public static bool ColinearAboutAxes(IEnumerable<Coordinate> coordinates)
        {
            if (null == coordinates || coordinates.Count() < 2)
            {
                throw new ArgumentException("Must have two or more coordinates to check for colinearity");
            }

            var firstCoord = coordinates.First();

            if (coordinates.All(c => c.X.Equals(firstCoord.X)))
            {
                return true;
            }

            if (coordinates.All(c => c.Y.Equals(firstCoord.Y)))
            {
                return true;
            }

            return false;
        }

    }
}
