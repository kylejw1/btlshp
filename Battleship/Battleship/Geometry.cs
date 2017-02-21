using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Battleship
{
    public static class Geometry
    {
        private static bool HorizontalColinear(IEnumerable<Point> points)
        {
            // Treat equal points as colinear

            if (null == points || points.Count() < 2)
            {
                throw new ArgumentException("Must have two or more points to check for colinearity");
            }

            return points.All(c => c.Y.Equals(points.First().Y));
        }

        private static bool VerticalColinear(IEnumerable<Point> points)
        {
            // Treat equal points as colinear

            if (null == points || points.Count() < 2)
            {
                throw new ArgumentException("Must have two or more points to check for colinearity");
            }

            return points.All(c => c.X.Equals(points.First().X));
        }

        public static bool ColinearAboutAxes(IEnumerable<Point> points)
        {
            if (null == points || points.Count() < 2)
            {
                throw new ArgumentException("Must have two or more coordinates to check for colinearity");
            }

            return HorizontalColinear(points) || VerticalColinear(points);
        }

    }
}
