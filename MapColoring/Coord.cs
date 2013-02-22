using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapColoring
{
    public struct Coord : IEqualityComparer<Coord>
    {
        // @enum
        public enum AdjacentCoord
        {
            Up,
            Down,
            Left,
            Right
        }

        // @Properties
        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }


        // @Public
        public Coord(int x = 0, int y = 0)
        {
            _x = x;
            _y = y;
        }

        public Coord GetAdjacentCoord(AdjacentCoord target)
        {
            switch (target)
            {
                case AdjacentCoord.Up:
                    return new Coord(_x, _y - 1);
                case AdjacentCoord.Down:
                    return new Coord(_x, _y + 1);
                case AdjacentCoord.Left:
                    return new Coord(_x - 1, _y);
                case AdjacentCoord.Right:
                    return new Coord(_x + 1, _y);
                default:
                    throw new ArgumentException("Invalid direction constant.");
            }
        }

        // @Private
        int _x;
        int _y;


        // @Operators overload
        public static bool operator== (Coord x, Coord y)
        {
            return (x.X == y.X && x.Y == y.Y);
        }

        public static bool operator!= (Coord x, Coord y)
        {
            return (x.X != y.X || x.Y != y.Y);
        }

        public bool Equals(Coord x, Coord y)
        {
            return (x == y);
        }

        public int GetHashCode(Coord obj)
        {
            return _x.GetHashCode() ^ _y.GetHashCode();
        }
    }
}
