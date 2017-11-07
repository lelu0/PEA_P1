using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEA1_Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            WorldMap WorldMap = new WorldMap(4);
            WorldMap.printCurrentMap();
            WorldMap.MatrixReduction(2,3);
            WorldMap.printCurrentMap();
        }


    }
}
