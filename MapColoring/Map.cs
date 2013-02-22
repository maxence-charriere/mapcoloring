using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapColoring
{
    public class Map
    {
        // @Properties
        public int Width
        {
            get
            {
                return _width;
            }
        }

        public int Heigth
        {
            get
            {
                return _mapList.Count;
            }
        }


        // @Indexer
        public char this[int x, int y]
        {
            get
            {
                return this[new Coord(x, y)];
            }
            set
            {
                this[new Coord(x, y)] = value;
            }
        }

        public char this[Coord pos]
        {
            get
            {
                if (!IsInside(pos))
                {
                    throw new ArgumentException(
                        string.Format("Indexer - Coord({0}, {1}) is out of the map.", pos.X, pos.Y));
                }

                return _mapList[pos.Y][pos.X];
            }
            set
            {
                if (!IsInside(pos))
                {
                    throw new ArgumentException(
                        string.Format("Indexer - Coord({0}, {1}) is out of the map.", pos.X, pos.Y));
                }

                _mapList[pos.Y][pos.X] = value;
            }
        }


        // @Public
        public Map(string path)
        {
            // Check the filepath.
            if (string.IsNullOrEmpty(path))
            {
            }

            // Read the file.
            using (var streamReader = new StreamReader(path))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    // Define map width.
                    line = line.Trim(' ');
                    if (_width == 0)
                    {
                        _width = line.Length;
                    }

                    // Check if map is valid.
                    CheckLine(line);

                    _mapList.Add(line.ToList());
                }
            }

            // Construct map explored state
            InitMapExploredState();
        }

        public bool IsInside(int x, int y)
        {
            return IsInside(new Coord(x, y));
        }

        public bool IsInside(Coord pos)
        {
            return (pos.X >= 0 && pos.X < Width &&
                pos.Y >= 0 && pos.Y < Heigth);
        }

        public bool IsExplored(int x, int y)
        {
            return IsExplored(new Coord(x, y));
        }

        public bool IsExplored(Coord pos)
        {
            if (!IsInside(pos))
            {
                throw new ArgumentException(
                    string.Format("IsExplored - Coord({0}, {1}) is out of the map.", pos.X, pos.Y));
            }
            return _mapExploredState[pos];
        }

        public void SetExplored(int x, int y)
        {
            SetExplored(new Coord(x, y));
        }

        public void SetExplored(Coord pos)
        {
            if (!IsInside(pos))
            {
                throw new ArgumentException(
                    string.Format("SetExplored - Coord({0}, {1}) is out of the map.", pos.X, pos.Y));
            }
            _mapExploredState[pos] = true;
        }

        public void Print()
        {
            Console.WriteLine(string.Format("{0} x {1}", Width, Heigth));
            foreach (var line in _mapList)
            {
                Console.WriteLine(new string(line.ToArray()));
            }
            Console.WriteLine("");
        }


        // @Private
        int                     _width = 0;
        List<List<char>>        _mapList = new List<List<char>>();
        Dictionary<Coord, bool> _mapExploredState;

        void CheckLine(string line)
        {
            // Check if the line length fit with the map length.
            if (line.Length != _width)
            {
                throw new Exception("The map must be a rectangle or a square.");
            }

            // Check if the line contain valid characters
            for (int i = 0; i < line.Length; ++i)
            {
                if (line[i] < 'a' || 'z' < line[i])
                {
                    throw new Exception("The map must contain only alphabetical ascii charaters");
                }
            }
        }

        void InitMapExploredState()
        {
            _mapExploredState = new Dictionary<Coord, bool>(Width * Heigth);
            for (int y = 0; y < Heigth; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    _mapExploredState[new Coord(x, y)] = false;
                }
            }
        }
    }
}
