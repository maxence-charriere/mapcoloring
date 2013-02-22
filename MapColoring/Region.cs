using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapColoring
{
    public class Region : IComparable<Region>
    {
        // @Properties
        public char CurrentValue
        {
            get;
            set;
        }

        public char Color
        {
            get
            {
                return _color;
            }
        }


        // @Public
        public Region(int x, int y, Map map) :  this(new Coord(x, y), map)
        {
        }

        public Region(Coord entryPoint, Map map)
        {
            CurrentValue = map[entryPoint];
            _map = map;
            Explore(entryPoint);
        }

        public int CompareTo(Region other)
        {
            return other._plots.Count - _plots.Count;
        }

        public bool ItInclude(Coord pos)
        {
            return _plots.Contains(pos);
        }

        public void IdentifyAdjacentRegions(List<Region> regions)
        {
            // Look and list the regions which share an edge with this object
            // by check to who belongs the adjacents Coord of this region.
            foreach (Coord plot in _plots)
            {
                // Look Up
                Region regionUp = FindOneByPosition(plot.GetAdjacentCoord(Coord.AdjacentCoord.Up), regions);
                if (regionUp != null && regionUp != this && !_adjacentRegions.Contains(regionUp))
                {
                    _adjacentRegions.Add(regionUp);
                }

                // Look Down
                Region regionDown = FindOneByPosition(plot.GetAdjacentCoord(Coord.AdjacentCoord.Down), regions);
                if (regionDown != null && regionDown != this && !_adjacentRegions.Contains(regionDown))
                {
                    _adjacentRegions.Add(regionDown);
                }

                // Look Left
                Region regionLeft = FindOneByPosition(plot.GetAdjacentCoord(Coord.AdjacentCoord.Left), regions);
                if (regionLeft != null && regionLeft != this && !_adjacentRegions.Contains(regionLeft))
                {
                    _adjacentRegions.Add(regionLeft);
                }

                // Look Right
                Region regionRight = FindOneByPosition(plot.GetAdjacentCoord(Coord.AdjacentCoord.Right), regions);
                if (regionRight != null && regionRight != this && !_adjacentRegions.Contains(regionRight))
                {
                    _adjacentRegions.Add(regionRight);
                }
            }
        }

        public static Region FindOneByPosition(Coord pos, List<Region> regions)
        {
            // Find a region in a list by a Coord.
            foreach (Region region in regions)
            {
                if (region.ItInclude(pos))
                {
                    return region;
                }
            }
            return null;
        }

        public void Colorize()
        {
            // Assign a valid color to the region
            if (!AdjacentRegionsContainColor('R'))
            {
                _color = 'R';
            }
            else if (!AdjacentRegionsContainColor('G'))
            {
                _color = 'G';
            }
            else if (!AdjacentRegionsContainColor('B'))
            {
                _color = 'B';
            }
            else if (!AdjacentRegionsContainColor('Y'))
            {
                _color = 'Y';
            }

            // Pianting the color on the map
            foreach (Coord plot in _plots)
            {
                _map[plot] = _color;
            }

            // Notify that the program cannot resolve the problem
            if (_color == ' ')
            {
                Console.WriteLine(string.Format("Cannot resolve color from region starting at ({0}, {1})", _plots[0].X, _plots[0].Y));
                Console.WriteLine("");
            }
        }


        // @Private
        Map             _map;
        char            _color = ' ';
        List<Coord>     _plots = new List<Coord>();
        List<Region>    _adjacentRegions = new List<Region>(3);

        bool IsUnexploredPos(Coord pos)
        {
            if (_map.IsInside(pos) && !_map.IsExplored(pos) && _map[pos] == CurrentValue)
            {
                return true;
            }
            return false;
        }

        void Explore(Coord pos)
        {
            // Update region informations
            _map.SetExplored(pos);
            _plots.Add(pos);

            // Look to North
            if (IsUnexploredPos(pos.GetAdjacentCoord(Coord.AdjacentCoord.Up)))
            {
                Explore(pos.GetAdjacentCoord(Coord.AdjacentCoord.Up));
            }

            // Look to East
            if (IsUnexploredPos(pos.GetAdjacentCoord(Coord.AdjacentCoord.Right)))
            {
                Explore(pos.GetAdjacentCoord(Coord.AdjacentCoord.Right));
            }

            // Look to South
            if (IsUnexploredPos(pos.GetAdjacentCoord(Coord.AdjacentCoord.Down)))
            {
                Explore(pos.GetAdjacentCoord(Coord.AdjacentCoord.Down));
            }

            // Look to West
            if (IsUnexploredPos(pos.GetAdjacentCoord(Coord.AdjacentCoord.Left)))
            {
                Explore(pos.GetAdjacentCoord(Coord.AdjacentCoord.Left));
            }

        }

        bool AdjacentRegionsContainColor(char color)
        {
            // Indentify if an adjacent region use the specified color.
            foreach (Region region in _adjacentRegions)
            {
                if (region.Color == color)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
