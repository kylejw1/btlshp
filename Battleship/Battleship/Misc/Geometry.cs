using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Battleship.Misc
{
    public static class Geometry
    {
        /// <summary>
        /// Check if points rest on the same horizontal line
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private static bool HorizontalColinear(IEnumerable<Point> points)
        {
            // Treat equal points as colinear

            if (null == points || points.Count() < 2)
            {
                throw new ArgumentException("Geometry :: HorizontalColinear :: Must have two or more points to check for colinearity");
            }

            return points.All(c => c.Y.Equals(points.First().Y));
        }

        /// <summary>
        /// Check if points rest on the same vertical line
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private static bool VerticalColinear(IEnumerable<Point> points)
        {
            // Treat equal points as colinear

            if (null == points || points.Count() < 2)
            {
                throw new ArgumentException("Geometry :: VerticalColinear :: Must have two or more points to check for colinearity");
            }

            return points.All(c => c.X.Equals(points.First().X));
        }

        /// <summary>
        /// Check if points rest on either a vertical or horizontal line
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static bool ColinearAboutAxes(IEnumerable<Point> points)
        {
            if (null == points || points.Count() < 2)
            {
                throw new ArgumentException("Goemetry :: ColinearAboutAxes :: Must have two or more coordinates to check for colinearity");
            }

            return HorizontalColinear(points) || VerticalColinear(points);
        }

    }
}
