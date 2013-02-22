using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapColoring
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || string.IsNullOrEmpty(args[0]))
            {
                Console.WriteLine("error: You must enter the filepath of the map.");
                return;
            }

            try
            {
                // Map Creation
                var map = new Map(args[0]);
                Console.WriteLine("Input Map :");
                map.Print();
                
                // Map Colorization
                var cartographer = new Cartographer(map);
                map = cartographer.ExploreAndColorize();
                Console.WriteLine("Colorized Map :");
                map.Print();
            }
            catch (Exception e)
            {
                Console.WriteLine("error: " + e.Message);
            }
        }
    }
}
