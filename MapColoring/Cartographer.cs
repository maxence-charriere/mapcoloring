using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapColoring
{
    public class Cartographer
    {
        // @Public
        public Cartographer(Map map)
        {
            _map = map;
            _availableColors.Push('Y');
            _availableColors.Push('B');
            _availableColors.Push('G');
            _availableColors.Push('R');
        }

        public Map ExploreAndColorize()
        {
            ExploreRegions();
            Colorize();
            return _map;
        }        

        // @Private
        Map             _map;
        Stack<char>     _availableColors = new Stack<char>(4);
        List<Region>    _regions = new List<Region>();

        void ExploreRegions()
        {
            // Explore the map;
            for (int y = 0; y < _map.Heigth; ++y)
            {
                for (int x = 0; x < _map.Width; ++x)
                {
                    if (!_map.IsExplored(x, y))
                    {
                        var region = new Region(x, y, _map);
                        _regions.Add(region);
                    }
                }
            }
        }

        private void Colorize()
        {
            // Identify for each region their adjacent regions.
            foreach (Region region in _regions)
            {
                region.IdentifyAdjacentRegions(_regions);
            }

            // Apply the color.
            _regions.Sort();
            foreach (Region region in _regions)
            {
                region.Colorize();
            }
        }
    }
}
